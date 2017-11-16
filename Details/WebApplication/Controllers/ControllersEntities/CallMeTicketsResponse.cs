using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    public class CallMeTicketsResponse
    {
        public CallMeTicket[] Tickets { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
