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
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;

        public OrdersController(IDetailRepository detailRepository, IOrderRepository orderRepository, IDeliveryMethodRepository deliveryMethodRepository)
        {
            _detailRepository = detailRepository;
            _orderRepository = orderRepository;
            _deliveryMethodRepository = deliveryMethodRepository;
        }

        #region POST {host}/СheckOrderAvailability - Проверка возможности оформления заказа

        [HttpPost("/ChechOrderAvailability")]
        public IActionResult ChechOrderAvailability([FromBody] CheckOrderAvailabilityRequest request)
        {
            var newOrder = new Order()
            {
                Id = -1,
                Items = request.Items,
                CustomerName = request.CustomerName,
                CustomerSurname = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                CustomerAddress = request.CustomerAddress,
                CustomerMail = request.CustomerMail,
                CustomerComment = request.CustomerComment,
                DeliveryMethodId = request.DeliveryMethodId,
                State = OrderState.New
            };
            try
            {
                var order = _orderRepository.Create(newOrder);
                var result = new CheckOrderAvailabilityResponse() { OrderId = order.Id };
                return Json(result);
            }
            catch (OrderAvailabilityException ex)
            {
                var result = new CheckOrderAvailabilityResponse() { ProblemItems = ex.ProblemItems };
                return Json(result);
            }
        }

        #endregion

        #region POST {host}/SelectPaymentMethod - Выбор типа оплаты конкретного заказа

        [HttpPost("/SelectPaymentMethod")]
        public IActionResult SelectPaymentMethod([FromBody] SelectPaymentMethodRequest request)
        {
            // todo kirill: реализовать выбор типа оплаты конкретного заказа
            var result = new SelectPaymentMethodResponse() { LinkToPayment = "TestLinkToPayment" };

            return Json(result);
        }

        #endregion

        #region GET {host}/CheckOrderState?orderId= - Проверка статуса конкретного заказа

        [HttpGet("/CheckOrderState")]
        public IActionResult CheckOrderState(int orderId)
        {
            var state = _orderRepository.CheckState(orderId);
            if (state != null)
            {
                var result = new OrderStateResponse() { State = state.Value };
                return Json(result);
            }

            return StatusCode(404);
        }

        #endregion

        #region GET {host}/GetDeliveryMethods - Получение вариантов доставки

        [HttpGet("/GetDeliveryMethods")]
        public IActionResult GetDeliveryMethods()
        {
            var methods =_deliveryMethodRepository.GetAll();
            var result = new GetDeliveryMethodsResponse() { methods = methods };

            return Json(result);
        }

        #endregion
    }
}
