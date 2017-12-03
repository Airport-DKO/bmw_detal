using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Repositories.Interfaces;
using WebApplication.Controllers.ControllersEntities;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления заявками "Перезвони мне"
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
        public IActionResult CreateCallMeTicket([FromBody] CallMeTicketCreateRequest callMeTicket)
        {
            // think about it: возможно, стоит провалидировать номер телефона и отправить StatusCode(400), если что-то не так

            _callMeTicketRepository.Create(callMeTicket);

            return StatusCode(200);
        }

        #endregion

        #region GET {host}/Admin/CallMeTickets - Получение списка заявок "Перезвони мне" (с пагинацией по 25)

        [Authorize]
        [HttpGet("/Admin/CallMeTickets")]
        public IActionResult GetCallMeTickets(int page)
        {
            var limit = 25;
            var skip = (page - 1) * 25;
            var tickets = _callMeTicketRepository.GetAll(skip, limit);
            var ticketsCount = _callMeTicketRepository.Count();

            var result = new CallMeTicketsResponse()
            {
                Tickets = tickets,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)ticketsCount / limit)
            };

            return Json(result);
        }

        #endregion

        #region PUT {host}/Admin/CallMeTickets/SetDone/{ticketId} - Перевод заявки "Перезвони мне" в состояние "Перезвонили"

        [Authorize]
        [HttpPut("/Admin/CallMeTickets/SetDone/{ticketId}")]
        public IActionResult SetCallMeTicketsDone(int ticketId)
        {
            var id = _callMeTicketRepository.SetDone(ticketId);
            return id != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion

        #region DELETE {host}/Admin/CallMeTickets/{ticketId} - Удаление заявки "Перезвони мне"

        [Authorize]
        [HttpDelete("/Admin/CallMeTickets/{ticketId}")]
        public IActionResult DeleteCallMeTickets(int ticketId)
        {
            var id = _callMeTicketRepository.Delete(ticketId);
            return id != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion
    }
}
