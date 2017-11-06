using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.Repositories.Interfaces;
using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления заказами
    /// </summary>
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IDetailRepository _detailRepository;

        public OrdersController(IDetailRepository detailRepository)
        {
            _detailRepository = detailRepository;
        }

        #region POST {host}/СheckOrderAvailability - Проверка возможности оформления заказа

        [HttpPost("/ChechOrderAvailability")]
        public JsonResult ChechOrderAvailability([FromBody] CheckOrderAvailabilityRequest request)
        {
            // todo nika: разобраться, почему не работает
            // todo nika: реализовать проверку возможности оформления заказа, при этом не забыть завалидировать в целом структуру, например неотрицательное количество элементов
            var result = new CheckOrderAvailabilityResponse() { OrderId = 1, ProblemItems = new OrderItem[] { new OrderItem() { InternalId = 1, Quantity = 10 } } };

            return Json(result);
        }

        #endregion

        #region POST {host}/SelectPaymentMethod - Выбор типа оплаты конкретного заказа

        [HttpPost("/SelectPaymentMethod")]
        public JsonResult SelectPaymentMethod([FromBody] SelectPaymentMethodRequest request)
        {
            // todo nika: реализовать выбор типа оплаты конкретного заказа
            var result = new SelectPaymentMethodResponse() { LinkToPayment = "TestLinkToPayment" };

            return Json(result);
        }

        #endregion

        #region GET {host}/CheckPayment?order= - Проверка оплаты конкретного заказа

        [HttpGet("/CheckPayment")]
        public JsonResult CheckPayment(string orderId)
        {
            // todo nika: реализовать проверку оплаты конкретного заказа
            var result = new OrderPaymentRespone() { Status = 10 };

            return Json(result);
        }

        #endregion

    }
}
