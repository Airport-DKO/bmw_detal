using System.Data;
using System.Linq;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.Controllers.ControllersEntities;
using Npgsql;
using Dapper;

namespace WebApplication.Repositories.Entities
{
    public class DetailRepository : BaseRepository, IDetailRepository
    {
        public DetailRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Быстрый поиск по частичному совпадению строки query со свойством detailnumber
        /// </summary>
        public QuickSearchResult[] QuickSearch(string query, bool isAdmin=false)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                query = $"%{query}%";
                var sqlQuery = "SELECT detailnumber, name as detailname " +
                    "FROM detail " +
                    "WHERE detailnumber LIKE @query " +
                    (isAdmin ? ";" : "AND isdeleted=false; ");
                return db.Query<QuickSearchResult>(sqlQuery, new { query }).ToArray();
            }
        }

        /// <summary>
        /// Полный поиск по частичному совпадению строки query со свойством detailnumber
        /// </summary>
        public SolidSearchResult[] SolidSearch(string query, bool isAdmin = false)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT internalid, type, detailnumber, originaldetailnumber, " +
                    "name as detailname, price, deliverytime as deliverytimeinfo, description, stockquantity as quantity " +
                    "FROM detail " +
                    "WHERE detailnumber=@query " +
                    (isAdmin ? ";" : "AND isdeleted=false; ");
                return db.Query<SolidSearchResult>(sqlQuery, new { query }).ToArray();
            }
        }

        /// <summary>
        /// Получение информации о детали по ее идентификатору
        /// </summary>
        public Detail GetById(int detailId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT internalid, type, detailnumber, originaldetailnumber, name, " +
                    "price, deliverytime as deliverytimeinfo, description, stockquantity as quantity, provider, isdeleted " +
                    "FROM detail " +
                    "WHERE internalid = @detailid";
                return db.Query<Detail>(sqlQuery, new { detailId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Обновление информации о детали
        /// </summary>
        public int? Update(Detail detail)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE detail SET " +
                    (detail.DetailNumber != null ? "detailnumber = @detailnumber, " : string.Empty) +
                    (detail.OriginalDetailNumber != null ? "originaldetailnumber = @originaldetailnumber, " : string.Empty) +
                    (detail.Name != null ? "name = @name, " : string.Empty) +
                    (detail.DeliveryTimeInfo != null ? "deliverytime = @deliverytimeinfo, " : string.Empty) +
                    (detail.Description != null ? "description = @description, " : string.Empty) +
                    (detail.Provider != null ? "provider = @provider, " : string.Empty) +
                    "type = @type, " +
                    "stockquantity = @quantity, " +
                    "price = @price, " +
                    "isdeleted = @isdeleted " +
                    "WHERE internalid = @internalid; " +
                    "" +
                    "SELECT internalid " +
                    "FROM detail " +
                    "WHERE internalid = @internalid;";
                return db.Query<int?>(sqlQuery, detail).FirstOrDefault();
            }
        }

        /// <summary>
        /// Пометить деталь как удаленную
        /// </summary>
        public int? MarkAsDeleted(int internalId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE detail " +
                    "SET " +
                    "isDeleted = true " +
                    "WHERE internalId = @internalId; " +
                    "" +
                    "SELECT internalId " +
                    "FROM detail " +
                    "WHERE internalId = @internalId;";
                return db.Query<int?>(sqlQuery, new { internalId }).FirstOrDefault();
            }
        }
    }
}