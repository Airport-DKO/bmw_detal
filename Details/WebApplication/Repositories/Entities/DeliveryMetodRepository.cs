using System.Data;
using Npgsql;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using Dapper;
using System.Linq;

namespace WebApplication.Repositories.Entities
{
    public class DeliveryMethodRepository : BaseRepository, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(string connectionString) : base(connectionString) { }

        public DeliveryMethod[] GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT id, locationtype, deliverytype, name as deliveryName, price as deliveryPrice " +
                    "FROM deliverymethod;";
                return db.Query<DeliveryMethod>(sqlQuery).ToArray();
            }
        }
    }
}
