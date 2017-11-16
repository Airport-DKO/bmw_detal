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

        #region GET {host}/admin/details/{internalId} - Просмотр информации о конкретном товаре

        [HttpGet("/admin/details/{internalId}")]
        public IActionResult GetDetail(int internalId)
        {
            // todo nika: предусмотреть необходимость авторизации

            var detail = _detailRepository.GetById(internalId);
            if (detail != null)
                return Json(detail);
            else
                return StatusCode(404);
        }

        #endregion

        #region PUT {host}/admin/details/{internalId} - Просмотр информации о конкретном товаре

        [HttpPut("/admin/details/{internalId}")]
        public IActionResult UpdateDetail(int internalId, [FromBody]Detail detail)
        {
            // todo nika: предусмотреть необходимость авторизации

            if (detail.InternalId!=internalId)
                return StatusCode(400);

            var id = _detailRepository.Update(detail);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion

        #region DELETE {host}/admin/details/{internalId} - Просмотр информации о конкретном товаре

        [HttpDelete("/admin/details/{internalId}")]
        public IActionResult DeleteDetail(int internalId)
        {
            // todo nika: предусмотреть необходимость авторизации
            
            var id = _detailRepository.MarkAsDeleted(internalId);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion
    }
}