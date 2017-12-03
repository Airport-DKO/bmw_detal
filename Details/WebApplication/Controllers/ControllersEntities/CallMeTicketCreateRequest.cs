using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    public class CallMeTicketCreateRequest
    {
        public string MobileNumber { get; set; }
        public CallMeTicketType Type { get; set; }
    }
}
