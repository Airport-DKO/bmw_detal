using Dapper;
using Npgsql;
using System.Data;
using System.Linq;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories.Entities
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString) { }

        public Order Create(Order order)
        {
            // todo nika: реализовать метод проверки и резервации заказа, в случае неудачи выкинуть OrderAvailabilityException
            throw new System.NotImplementedException();
        }

        public OrderState? CheckState(int orderId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT orderstate " +
                    "FROM \"order\" " +
                    "WHERE id=@orderId;";
                return db.Query<OrderState?>(sqlQuery, new { orderId }).FirstOrDefault();
            }
        }
    }
}
