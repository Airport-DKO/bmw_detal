using WebApplication.Controllers.ControllersEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Получение списка заказов
        /// </summary>
        ShortOrder[] GetAll(int skip, int limit, OrderState[] states);

        /// <summary>
        /// Получение детальной информации о заказе
        /// </summary>
        OrderDetails GetById(int orderId);

        /// <summary>
        /// Создание заказа
        /// </summary>
        int Create(CheckOrderAvailabilityRequest order);

        /// <summary>
        /// Обновление информации о заказе
        /// </summary>
        int? Update(OrderDetails order);

        /// <summary>
        /// Получение статуса заказа
        /// </summary>
        OrderState? GetOrderState(int orderId);

        /// <summary>
        /// Получение количества заказов
        /// </summary>
        int Count(OrderState[] states);
        


        /// <summary>
        /// Получение списка деталей в заказе
        /// </summary>
        OrderItemDetails[] GetOrderItems(int orderId);

        /// <summary>
        /// Добавление деталей к заказу
        /// </summary>
        int? AddOrderItem(OrderItem orderItem);

        /// <summary>
        /// Обновление детали в заказе
        /// </summary>
        int? UpdateOrderItem(OrderItem orderItem);

        /// <summary>
        /// Удаление детали из заказа
        /// </summary>
        int? DeleteOrderItem(int orderId, int orderItemId);
    }
}