using System.Data;
using System.Linq;
using Dapper;
using Npgsql;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories.Entities
{
    public class DetailRepository : BaseRepository, IDetailRepository
    {
        public DetailRepository(string connectionString) : base(connectionString) { }

        public QuickSearchResult[] QuickSearch(string query)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            { // think about it: для клиентов поиск по неудаленным, для админки - поиск по всем
                query = $"%{query}%";
                var sqlQuery = "SELECT detailnumber, name as detailName " +
                    "FROM detail " +
                    "WHERE detailnumber LIKE @query " +
                    "AND isDeleted=false; ";
                return db.Query<QuickSearchResult>(sqlQuery, new { query }).ToArray();
            }
        }

        public SolidSearchResult[] SolidSearch(string query)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            { // think about it: для клиентов поиск по неудаленным, для админки - поиск по всем
                var sqlQuery = "SELECT internalid, type, detailnumber, originaldetailnumber, " +
                    "name as detailname, price, deliverytime, description, stockquantity as quantity" +
                    "FROM detail " +
                    "WHERE detailnumber=@query " +
                    "AND isDeleted = false; ";
                return db.Query<SolidSearchResult>(sqlQuery, new { query }).ToArray();
            }
        }

        public Detail GetById(int internalId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT internalId, type, detailNumber, originalDetailNumber, name, " +
                    "price, deliveryTime, description, stockquantity as quantity, provider, isDeleted " +
                    "FROM detail " +
                    "WHERE internalId = @internalId";
                return db.Query<Detail>(sqlQuery, new { internalId }).FirstOrDefault();
            }
        }

        public int? Update(Detail detail)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE detail SET " +
                    (detail.DetailNumber != null ? "detailNumber = @detailNumber, " : string.Empty) +
                    (detail.OriginalDetailNumber != null ? "originalDetailNumber = @originalDetailNumber, " : string.Empty) +
                    (detail.Name != null ? "name = @name, " : string.Empty) +
                    (detail.DeliveryTime != null ? "deliveryTime = @deliveryTime, " : string.Empty) +
                    (detail.Description != null ? "description = @description, " : string.Empty) +
                    (detail.Provider != null ? "provider = @provider, " : string.Empty) +
                    "type = @type, " +
                    "stockquantity = @stockquantity, " +
                    "price = @price, " +
                    "isDeleted = @isDeleted " +
                    "WHERE internalId = @internalId; " +
                    "SELECT internalId FROM detail WHERE internalId = @internalId;";
                return db.Query<int?>(sqlQuery, detail).FirstOrDefault();
            }
        }

        public int? MarkAsDeleted(int internalId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE detail SET " +
                    "isDeleted = true " +
                    "WHERE internalId = @internalId; " +
                    "SELECT internalId FROM detail WHERE internalId = @internalId;";
                return db.Query<int?>(sqlQuery, new { internalId }).FirstOrDefault();
            }
        }
    }
}