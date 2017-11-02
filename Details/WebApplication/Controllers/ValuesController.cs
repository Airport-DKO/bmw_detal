using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Controllers
{
    /// <summary>
    /// todo nika: удалить в дальнейшем тестовый контроллер
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDetailRepository _repository;

        public ValuesController(IDetailRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var list = _repository.GetDetails();
            foreach (var detail in list)
                Debug.WriteLine(detail.DetailNumber, detail.Delivery);
            return Json(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}