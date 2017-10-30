using Microsoft.AspNetCore.Mvc;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")] 
    public class SearchController : Controller
    {
        private readonly IDetailRepository _detailRepository;

        public SearchController(IDetailRepository repo)
        {
            _detailRepository = repo;
        }
        
        [HttpGet("quick/{q}")] // если без / в начале - то адресс относительно адреса контроллера
        public JsonResult QuickSearch(string q)
        {
            var details=_detailRepository.GetDetails();
            return Json(details);
        }
    }
}