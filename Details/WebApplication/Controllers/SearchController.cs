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
        public IActionResult QuickSearch(string q)
        {
            var details = _detailRepository.QuickSearch(q);
            var result = new QuickSearchResponse() { Details = details };

            return Json(result);
        }

        #endregion

        #region GET {host}/solid?q= - Полный поиск

        [HttpGet("/solid")]
        public IActionResult SolidSearch(string q)
        {
            var details = _detailRepository.SolidSearch(q);
            var result = new SolidSearchResponse() { Details = details };

            return Json(result);
        }

        #endregion
    }
}