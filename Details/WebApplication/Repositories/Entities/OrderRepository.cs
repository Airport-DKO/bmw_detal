using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using WebApplication.Controllers;
using WebApplication.Repositories.Interfaces;
using WebApplication.Controllers.ControllersEntities;
using Npgsql;
using Dapper;

// todo nika: требуется тщательное рассмотрение этого репозитория и связанного с ним контроллера

namespace WebApplication.Repositories.Entities
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Получение списка заказов
        /// </summary>
        public ShortOrder[] GetAll(int skip, int limit, OrderState[] states)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить
                var sqlQuery = "SELECT id as orderid, customername, to_char(orderdate, 'dd.mm.yyyy') as date, orderstate as state, " +
                "(SELECT sum(quantity*price) FROM orderitem WHERE orderid=\"order\".id) as sum " +
                "FROM \"order\" " +
                "WHERE orderstate in @states " +
                "LIMIT @limit OFFSET @offset;";
                return db.Query<ShortOrder>(sqlQuery, new { skip, limit, states }).ToArray();
            }
        }

        /// <summary>
        /// Получение детальной информации о заказе
        /// </summary>
        public OrderDetails GetById(int orderId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить
                var sqlQuery = "SELECT o.d as id, o.customername, to_char(o.orderdate, 'dd.mm.yyyy') as date, o.orderstate as state, " +
                    "o.customersurname, o.customerphone, o.customerAdress, o.customerMail, o.customercomment, o.paymenttype " +
                    "d.deliverytype, d.price as deliveryprice " +
                    "FROM \"order\" o " +
                    "JOIN deliverymethod d ON o.deliverymethodid=d.id " +
                    "WHERE o.id=@orderid;";
                return db.Query<OrderDetails>(sqlQuery, new { orderId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        public int Create(CheckOrderAvailabilityRequest order)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить

                var transaction = db.BeginTransaction();

                var state = OrderState.New;
                var date = DateTime.Now;

                var sqlQuery = "INSERT INTO \"order\" (id, customername, customersurname, customerphone, customeraddress, " +
                    "customermail, customercomment, deliverymethodid, state, date) " +
                    "OUTPUT INSERTED.id" +
                    "VALUES(default, @customername, @customersurname, @customerphone, @customeraddress, " +
                    "@customermail, @customercomment, @deliverymethodid, @state, @date); ";
                var orderId = db.Query<int>(sqlQuery, new { order, state, date }).FirstOrDefault();

                var problemItems = new List<OrderItem>();
                foreach (var item in order.Items)
                {
                    item.OrderId = orderId;

                    sqlQuery = "SELECT quantity-@quantity FROM details WHERE internalId=@detailid; ";
                    var quantity = db.Query<int>(sqlQuery, item).FirstOrDefault();
                    if (quantity >= 0)
                    {
                        sqlQuery = "UPDATE details SET quantity=quantity-@quantity WHERE internalId=@detailid; ";
                        db.Execute(sqlQuery, item);

                        sqlQuery = "INSERT INTO orderitem (id, orderid, detailid, quantity, price) " +
                            "VALUES(default, @orderid, @detailid, @quantity, " +
                            "(SELECT price FROM detail WHERE id=@detailid)); ";
                        db.Execute(sqlQuery, item);
                    }
                    else
                    {
                        problemItems.Add(item);
                    }
                }

                if (problemItems.Count > 0)
                {
                    throw new OrderAvailabilityException(problemItems.ToArray());
                }
                else
                {
                    transaction.Commit();
                    return orderId;
                }
            }
        }

        /// <summary>
        /// Обновление информации о заказе
        /// </summary>
        public int? Update(OrderDetails order)
        {
            // todo nika: проверить

            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var transaction = db.BeginTransaction();

                var currentOrder = GetById(order.Id);

                // заказ не найден
                if (currentOrder == null)
                    return null;

                // возврат из состояния отмены - ошибка
                if (currentOrder.State == OrderState.Canceled && order.State != OrderState.Canceled)
                    throw new OrderAvailabilityException("Невозможно восстановить отмененный заказ. Создайте новый заказ.");

                // отмена заказа - возвращаем в продажу забронированные товары
                if (currentOrder.State != OrderState.Canceled && order.State == OrderState.Canceled)
                {
                    var currentOrderItems = GetOrderItems(order.Id);
                    foreach (var item in currentOrderItems)
                    {
                        var sqlOrderItemUpdateQuery = "UPDATE details SET quantity=quantity+@quantity WHERE internalId=@detailid; ";
                        db.Execute(sqlOrderItemUpdateQuery, item);

                        sqlOrderItemUpdateQuery = "DELETE FROM orderitem WHERE internalId=@internalId AND id=@orderItemId; ";
                        db.Execute(sqlOrderItemUpdateQuery, item);
                    }
                }

                // остальные случаи - просто обновляем строку в таблице
                var sqlQuery = "UPDATE \"order\" " +
                    "SET " +
                    "customername=@customername, " +
                    "customersurname=@customersurname, " +
                    "customerphone=@customerphone, " +
                    "customeradress=@customeradress, " +
                    "customermail=@customermail, " +
                    "customercomment=@customercomment, " +
                    "deliverymethodid=@deliverymethodid, " +
                    "paymenttype=@paymentType, " +
                    "state=@state " +
                    "WHERE o.id=@id;";

                db.Execute(sqlQuery, order);

                transaction.Commit();

                return order.Id;
            }
        }

        /// <summary>
        /// Получение статуса заказа
        /// </summary>
        public OrderState? GetOrderState(int orderId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT orderstate " +
                    "FROM \"order\" " +
                    "WHERE id=@orderid;";
                return db.Query<OrderState?>(sqlQuery, new { orderId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Получение количества заказов
        /// </summary>
        public int Count(OrderState[] states)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить

                var sqlQuery = "SELECT count(id) " +
                "FROM \"order\" " +
                "WHERE orderstate in @states;";
                return db.Query<int>(sqlQuery, new { states }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Получение списка деталей в заказе
        /// </summary>
        public OrderItemDetails[] GetOrderItems(int orderId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить
                var sqlQuery = "SELECT i.orderid as orderitemid, d.internalid, d.type, " +
                    "d.detailnumber, d.originaldetailnumber, d.name as detailname, i.price, i.quantity, d.deliverytime as deliverytimeinfo" +
                    "FROM orderitem i " +
                    "JOIN details d ON i.detailid=d.internalid " +
                    "WHERE o.id=@orderid;";
                return db.Query<OrderItemDetails>(sqlQuery, new { orderId }).ToArray();
            }
        }

        /// <summary>
        /// Добавление деталей к заказу
        /// </summary>
        public int? AddOrderItem(OrderItem orderItem)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var transaction = db.BeginTransaction();

                var sqlQuery = "SELECT quantity-@quantity FROM details WHERE internalId=@detailid; ";
                var quantity = db.Query<int>(sqlQuery, orderItem).FirstOrDefault();
                if (quantity >= 0)
                {
                    sqlQuery = "SELECT id FROM \"order\" WHERE id=@orderid;";
                    var orderId = db.Query<int?>(sqlQuery, orderItem).FirstOrDefault();
                    if (orderId != null)
                    {
                        sqlQuery = "UPDATE details SET quantity=quantity-@quantity WHERE internalId=@detailid; ";
                        db.Execute(sqlQuery, orderItem);

                        sqlQuery = "INSERT INTO orderitem (id, orderid, detailid, quantity, price) " +
                            "VALUES(default, @orderid, @detailid, @quantity, @price); ";
                        db.Execute(sqlQuery, orderItem);
                    }

                    transaction.Commit();

                    return orderId;
                }
                else
                {
                    throw new OrderAvailabilityException(new []{ orderItem });
                }
            }
        }

        /// <summary>
        /// Обновление детали в заказе
        /// </summary>
        public int? UpdateOrderItem(OrderItem orderItem)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var transaction = db.BeginTransaction();
                
                var sqlQuery = "SELECT quantity-@quantity FROM details WHERE internalId=@detailid; ";
                var quantity = db.Query<int>(sqlQuery, orderItem).FirstOrDefault();
                if (quantity >= 0)
                {
                    sqlQuery = "SELECT id FROM \"order\" WHERE id=@orderid;";
                    var orderId = db.Query<int?>(sqlQuery, orderItem).FirstOrDefault();
                    if (orderId != null)
                    {
                        sqlQuery = "UPDATE details SET quantity=quantity-@quantity WHERE internalId=@detailid; ";
                        db.Execute(sqlQuery, orderItem);

                        sqlQuery = "UPDATE orderitem " +
                            "SET " +
                            "quantity=@quantity, " +
                            "price=@price " +
                            "WHERE id=@id " +
                            "AND orderid=@orderid " +
                            "AND detailid=@detailid; " +
                            "SELECT id FROM orderitem WHERE id=@id;";
                        return db.Query<int?>(sqlQuery, orderItem).FirstOrDefault();
                    }

                    transaction.Commit();

                    return orderId;
                }
                else
                {
                    throw new OrderAvailabilityException(new[] { orderItem });
                }
            }
        }

        /// <summary>
        /// Удаление детали из заказа
        /// </summary>
        public int? DeleteOrderItem(int orderId, int orderItemId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить
                var sqlQuery = "SELECT id FROM \"order\" WHERE id=@orderid;";
                var id = db.Query<int?>(sqlQuery, new { orderId }).FirstOrDefault();
                if (id != null)
                {
                    sqlQuery = "SELECT id FROM orderitem WHERE id=@orderitemid AND orderid=@orderid;";
                    id = db.Query<int?>(sqlQuery, new { orderItemId, orderId }).FirstOrDefault();
                    sqlQuery = "DELETE FROM orderitem " +
                        "WHERE id = @orderitemid AND orderid=@orderid; ";
                    db.Execute(sqlQuery, new { orderId, orderItemId });
                }
                return orderId;
            }
        }
    }
}
