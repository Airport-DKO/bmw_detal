using System.Data;
using System.Linq;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using Npgsql;
using Dapper;

namespace WebApplication.Repositories.Entities
{
    public class DeliveryMethodRepository : BaseRepository, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Получение методов доставки
        /// </summary>
        public DeliveryMethod[] GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT id, locationtype, deliverytype, name as deliveryname, price " +
                    "FROM deliverymethod;";
                return db.Query<DeliveryMethod>(sqlQuery).ToArray();
            }
        }
    }
}
