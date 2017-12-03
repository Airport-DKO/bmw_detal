using System.Data;
using System.Linq;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.Controllers.ControllersEntities;
using Npgsql;
using Dapper;

namespace WebApplication.Repositories.Entities
{
    public class CallMeTicketRepository : BaseRepository, ICallMeTicketRepository
    {
        public CallMeTicketRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Создание заявки "Перезвони мне"
        /// </summary>
        public void Create(CallMeTicketCreateRequest callMeTicket)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: возможно, решить проблему: enum сохраняется как int
                var sqlQuery = "INSERT INTO callmeticket (id, mobilenumber, type, isnew) " +
                    "VALUES(default, @mobileNumber, @type, default); ";
                db.Execute(sqlQuery, callMeTicket);
            }
        }

        /// <summary>
        /// Получение списка заявок "Перезвони мне"
        /// </summary>
        public CallMeTicket[] GetAll(int offset, int limit)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT id, mobilenumber, type, isnew " +
                    "FROM callmeticket " +
                    "ORDER BY id " +
                    "LIMIT @limit OFFSET @offset; ";
                return db.Query<CallMeTicket>(sqlQuery, new { limit, offset }).ToArray();
            }
        }

        /// <summary>
        /// Получение количества заявок "Перезвони мне"
        /// </summary>
        public int Count()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT count(id) " +
                    "FROM callmeticket; ";
                return db.Query<int>(sqlQuery).FirstOrDefault();
            }
        }

        /// <summary>
        /// Пометить заявку "Перезвони мне" исполненной
        /// </summary>
        public int? SetDone(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE callmeticket " +
                    "SET " +
                    "isnew=false " +
                    "WHERE id=@id; " +
                    "" +
                    "SELECT id " +
                    "FROM callmeticket " +
                    "WHERE id=@id; ";
                return db.Query<int?>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Удалить заявку "Перезвони мне"
        /// </summary>
        public int? Delete(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT id " +
                    "FROM callmeticket " +
                    "WHERE id=@id; " +
                    "" +
                    "DELETE FROM callmeticket " +
                    "WHERE id=@id; ";
                return db.Query<int?>(sqlQuery, new { id }).FirstOrDefault();
            }
        }
    }
}
