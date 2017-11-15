using WebApplication.Controllers.ControllersEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDetailRepository
    {
        QuickSearchResult[] QuickSearch(string query);
        SolidSearchResult[] SolidSearch(string query);
    }
}