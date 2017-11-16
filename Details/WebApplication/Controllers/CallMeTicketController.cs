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
    public class CallMeTicketController : Controller
    {
        private readonly ICallMeTicketRepository _callMeTicketRepository;

        public CallMeTicketController(ICallMeTicketRepository callMeTicketRepository)
        {
            _callMeTicketRepository = callMeTicketRepository;
        }

        #region POST {host}/CallMeTicket - Создание заявки "Перезвони мне"

        [HttpPost("/CallMeTicket")]
        public IActionResult CreateCallMeTicket([FromBody] CallMeTicketRequest request)
        {
            // think about it: возможно, стоит провалидировать номер телефона
            var callMeTicket = new CallMeTicket()
            {
                MobileNumber = request.MobileNumber,
                Type = request.Type,
                IsNew = true
            };
            _callMeTicketRepository.Create(callMeTicket);
            // think about it: как отправить код ответа, если будем валидировать номер телефона?

            return StatusCode(200);
        }

        #endregion

        #region GET {host}/admin/CallMeTickets - Получение списка заявок "Перезвони мне" (с пагинацией по 25)

        [HttpGet("/admin/CallMeTickets")]
        public IActionResult GetCallMeTickets(int page)
        {
            // todo nika: предусмотреть необходимость авторизации

            var limit = 25;
            var skip = (page - 1) * 25;
            var tickets = _callMeTicketRepository.GetAll(skip, limit);
            var ticketsCount = _callMeTicketRepository.Count();

            var result = new CallMeTicketsResponse()
            {
                Tickets = tickets,
                CurrentPage = page,
                TotalPages = ticketsCount
            };

            return Json(result);
        }

        #endregion

        #region PUT {host}/admin/CallMeTickets/SetDone/{ticketId} - Перевод заявки "Перезвони мне" в состояние "перезвонили"

        [HttpPut("/admin/CallMeTickets/SetDone/{ticketId}")]
        public IActionResult SetCallMeTicketsDone(int ticketId)
        {
            // todo nika: предусмотреть необходимость авторизации

            var id = _callMeTicketRepository.SetDone(ticketId);
            return id != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion

        #region DELETE {host}/admin/CallMeTickets/{ticketId} - Удаление заявки "Перезвони мне"

        [HttpDelete("/admin/CallMeTickets/{ticketId}")]
        public IActionResult DeleteCallMeTickets(int ticketId)
        {
            // todo nika: предусмотреть необходимость авторизации

            var id = _callMeTicketRepository.Delete(ticketId);
            return id != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion
    }
}
