using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер поиска
    /// </summary>
    [Route("api/[controller]")] 
    public class SearchController : Controller
    {
        private readonly IDetailRepository _detailRepository;

        public SearchController(IDetailRepository detailRepository)
        {
            _detailRepository = detailRepository;
        }

        #region GET {host}/?q= - Быстрый поиск

        [HttpGet("/")]
        public JsonResult QuickSearch(string q)
        {
            // todo nika: реализовать быстрый поиск
            var result = new QuickSearchResponse() { Details = new QuickSearchResult[] { new QuickSearchResult() { DetailNumber = "TestNumber", DetailName = "TestName" } } };

            return Json(result);
        }

        #endregion

        #region GET {host}/solid?q= - Полный поиск

        // todo nika: реализовать полный поиск

        [HttpGet("/solid")]
        public JsonResult SolidSearch(string q)
        {
            // todo nika: реализовать полный поиск
            var result = new SolidSearchResponse()
            {
                Details = new SolidSearchResult[] { new SolidSearchResult() {
                InternalId =1,
                Type = DetailType.Analog,
                DetailNumber = "TestNumber",
                OriginalDetailNumber = "TestOriginalDetailNumber",
                DetailName = "TestDetailName",
                Price = 1000000.10m,
                Delivery = "TestDelivery",
                Description = "TestDescription",
                Quantity = 8} }
            };

            return Json(result);
        }

        #endregion
    }
}