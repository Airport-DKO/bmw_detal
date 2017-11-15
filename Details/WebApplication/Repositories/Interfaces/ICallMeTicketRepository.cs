using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICallMeTicketRepository
    {
        void Create(CallMeTicket request);
    }
}
