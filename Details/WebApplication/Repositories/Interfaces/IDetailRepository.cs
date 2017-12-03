using WebApplication.DatabaseEntities;
using WebApplication.Controllers.ControllersEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDetailRepository
    {
        /// <summary>
        /// Быстрый поиск по частичному совпадению строки query со свойством detailnumber
        /// </summary>
        QuickSearchResult[] QuickSearch(string query, bool isAdmin = false);

        /// <summary>
        /// Полный поиск по частичному совпадению строки query со свойством detailnumber
        /// </summary>
        SolidSearchResult[] SolidSearch(string query, bool isAdmin = false);

        /// <summary>
        /// Получение информации о детали по ее идентификатору
        /// </summary>
        Detail GetById(int detailId);

        /// <summary>
        /// Обновление информации о детали
        /// </summary>
        int? Update(Detail detail);

        /// <summary>
        /// Пометить деталь как удаленную
        /// </summary>
        int? MarkAsDeleted(int internalId);
    }
}