using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления обратной связью
    /// </summary>
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly ICallMeTicketRepository _callMeTicketRepository;

        public FeedbackController(ICallMeTicketRepository callMeTicketRepository)
        {
            _callMeTicketRepository = callMeTicketRepository;
        }

        #region POST {host}/CallMeTicket - Заявка на обратную связь (перезвоните мне)

        [HttpPost("/CallMeTicket")]
        public void CreateCallMeTicket([FromBody] CallMeTicket request)
        {
            // think about it: возможно, стоит провалидировать номер телефона
            _callMeTicketRepository.Create(request);
            // think about it: как отправить код ответа, если будем валидировать номер телефона?
        }

        #endregion
    }
}
