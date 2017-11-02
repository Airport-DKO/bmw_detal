using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Npgsql;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories.Entities
{
    public class DetailRepository : BaseRepository, IDetailRepository
    {
        public DetailRepository(string connectionString) : base(connectionString) { }

        public void Create(Detail detail)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO Detail (Type, DetailNumber,OriginalDetailNumber," +
                               "Name,Price,Delivery,Description,StockQuantity) " +
                               "VALUES(@Type, @DetailNumber,@OriginalDetailNumber," +
                               "@Name,@Price,@Delivery,@Description,@StockQuantity); " +
                               "SELECT CAST(SCOPE_IDENTITY() as int)"; // для получения ID созданного объекта
                int? internalId = db.Query<int>(sqlQuery, detail).FirstOrDefault();
                detail.InternalId = internalId.Value;
            }
        }

        public void Delete(int internalId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Detail WHERE internalId = @internalId";
                db.Execute(sqlQuery, new {internalId});
            }
        }

        public Detail Get(int internalId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return db.Query<Detail>("SELECT * FROM Detail WHERE internalId = @internalId", new {internalId})
                    .FirstOrDefault();
            }
        }

        public List<Detail> GetDetails()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return db.Query<Detail>("SELECT * FROM Detail").ToList();
            }
        }

        public void Update(Detail detail)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Detail SET Type = @Type, DetailNumber = @DetailNumber," +
                               "OriginalDetailNumber=@OriginalDetailNumber," +
                               "Name=@Name, Price=@Price, Delivery=@Delivery," +
                               "Description=@Description, StockQuantity=@StockQuantity " +
                               "WHERE InternalId = @InternalId";
                db.Execute(sqlQuery, detail);
            }
        }
    }
}