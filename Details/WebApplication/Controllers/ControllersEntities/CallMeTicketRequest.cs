using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    public class CallMeTicketRequest
    {
        public string MobileNumber { get; set; }
        public CallMeTicketType Type { get; set; }
    }
}
