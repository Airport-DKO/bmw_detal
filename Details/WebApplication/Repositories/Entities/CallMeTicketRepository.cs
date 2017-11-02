using System.Data;
using Npgsql;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories.Entities
{
    public class CallMeTicketRepository : BaseRepository, ICallMeTicketRepository
    {
        public CallMeTicketRepository(string connectionString) : base(connectionString) { }

        public void Create(CallMeTicket callMeTicket)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                // todo nika: реализовать метод сохранения тикета в БД
            }
        }
    }
}
