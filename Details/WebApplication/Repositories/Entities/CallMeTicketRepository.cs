using System.Data;
using Npgsql;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using Dapper;
using System.Linq;

namespace WebApplication.Repositories.Entities
{
    public class CallMeTicketRepository : BaseRepository, ICallMeTicketRepository
    {
        public CallMeTicketRepository(string connectionString) : base(connectionString) { }

        public void Create(CallMeTicket callMeTicket)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: решить проблему: enum сохраняется как int
                var sqlQuery = "INSERT INTO callmeticket (id, mobilenumber, type, isnew) " +
                    "VALUES(default, @MobileNumber, @Type, @IsNew); ";
                db.Execute(sqlQuery, callMeTicket);
            }
        }

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


        public int Count()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT count(id) " +
                    "FROM callmeticket; ";
                return db.Query<int>(sqlQuery).FirstOrDefault();
            }
        }

        public int? SetDone(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE callmeticket " +
                    "SET isnew=false " +
                    "WHERE id=@id; " +
                    "SELECT id FROM callmeticket WHERE id=@id; ";
                return db.Query<int?>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public int? Delete(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT id FROM callmeticket WHERE id=@id; " +
                    "DELETE FROM callmeticket " +
                    "WHERE id=@id; ";
                return db.Query<int?>(sqlQuery, new { id }).FirstOrDefault();
            }
        }
    }
}
