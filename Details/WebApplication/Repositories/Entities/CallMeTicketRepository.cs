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
                var sqlQuery = "INSERT INTO callmeticket (mobilenumber, type, state) " +
                    "VALUES(@MobileNumber, @Type, @State);";
                db.Execute(sqlQuery, callMeTicket);
            }
        }

        public CallMeTicket[] GetNew()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: проверить
                var state = CallMeTicketState.New.ToString();
                var sqlQuery = "SELECT mobilenumber, type, state " +
                    "FROM callmeticket" +
                    "WHERE state=@state;";
                return db.Query<CallMeTicket>(sqlQuery, new { state }).ToArray();
            }
        }
    }
}
