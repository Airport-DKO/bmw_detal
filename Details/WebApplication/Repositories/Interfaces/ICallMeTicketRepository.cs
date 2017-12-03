using WebApplication.Controllers.ControllersEntities;
using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICallMeTicketRepository
    {
        /// <summary>
        /// Создание заявки "Перезвони мне"
        /// </summary>
        void Create(CallMeTicketCreateRequest request);

        /// <summary>
        /// Получение списка заявок "Перезвони мне"
        /// </summary>
        CallMeTicket[] GetAll(int skip, int limit);

        /// <summary>
        /// Получение количества заявок "Перезвони мне"
        /// </summary>
        int Count();

        /// <summary>
        /// Пометить заявку "Перезвони мне" исполненной
        /// </summary>
        int? SetDone(int id);

        /// <summary>
        /// Удалить заявку "Перезвони мне"
        /// </summary>
        int? Delete(int id);
    }
}
