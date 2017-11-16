using WebApplication.Controllers.ControllersEntities;
using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDetailRepository
    {
        QuickSearchResult[] QuickSearch(string query);
        SolidSearchResult[] SolidSearch(string query);
        Detail GetById(int internalId);
        int? Update(Detail detail);
        int? MarkAsDeleted(int internalId);
    }
}