using Microsoft.AspNetCore.Mvc;
using WebApplication.DatabaseEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.Controllers.ControllersEntities;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления деталями
    /// </summary>
    [Route("api/[controller]")] 
    public class DetailsController : Controller
    {
        private readonly IDetailRepository _detailRepository;

        public DetailsController(IDetailRepository detailRepository)
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

        #region GET {host}/Admin/?q= - Быстрый поиск в админке

        [Authorize]
        [HttpGet("/Admin/")]
        public IActionResult QuickSearchAdmin(string q)
        {
            var details = _detailRepository.QuickSearch(q, isAdmin: true);
            var result = new QuickSearchResponse() { Details = details };

            return Json(result);
        }

        #endregion

        #region GET {host}/Admin/solid?q= - Полный поиск в админке

        [HttpGet("/Admin/solid")]
        public IActionResult SolidSearchAdmin(string q)
        {
            var details = _detailRepository.SolidSearch(q, isAdmin: true);
            var result = new SolidSearchResponse() { Details = details };

            return Json(result);
        }

        #endregion

        #region GET {host}/Admin/Details/{id} - Просмотр информации о конкретном товаре

        [Authorize]
        [HttpGet("/Admin/Details/{id}")]
        public IActionResult GetDetail(int id)
        {
            var detail = _detailRepository.GetById(id);
            if (detail != null)
                return Json(detail);
            else
                return StatusCode(404);
        }

        #endregion

        #region PUT {host}/Admin/Details/{id} - Просмотр информации о конкретном товаре

        [Authorize]
        [HttpPut("/Admin/Details/{id}")]
        public IActionResult UpdateDetail(int id, [FromBody]Detail detail)
        {
            if (detail.InternalId != id)
                return StatusCode(400);

            var detailId = _detailRepository.Update(detail);
            return detailId != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion

        #region DELETE {host}/Admin/Details/{id} - Просмотр информации о конкретном товаре

        [Authorize]
        [HttpDelete("/Admin/Details/{id}")]
        public IActionResult DeleteDetail(int id)
        {
            var detailId = _detailRepository.MarkAsDeleted(id);
            return detailId != null ? StatusCode(200) : StatusCode(404);
        }

        #endregion
    }
}