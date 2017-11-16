using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICallMeTicketRepository
    {
        void Create(CallMeTicket request);
        CallMeTicket[] GetAll(int skip, int limit);
        int Count();
        int? SetDone(int id);
        int? Delete(int id);
    }
}
