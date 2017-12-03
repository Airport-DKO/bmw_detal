using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.ControllersEntities;
using WebApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления заказами
    /// </summary>
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;

        public OrdersController(IDetailRepository detailRepository, IOrderRepository orderRepository, IDeliveryMethodRepository deliveryMethodRepository)
        {
            _orderRepository = orderRepository;
            _deliveryMethodRepository = deliveryMethodRepository;
        }

        #region POST {host}/СheckOrderAvailability - Проверка возможности оформления заказа

        [HttpPost("/ChechOrderAvailability")]
        public IActionResult ChechOrderAvailability([FromBody] CheckOrderAvailabilityRequest request)
        {
            try
            {
                var orderId = _orderRepository.Create(request);
                var result = new CheckOrderAvailabilityResponse() { OrderId = orderId };
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
            var state = _orderRepository.GetOrderState(orderId);
            if (state != null)
            {
                var result = new OrderStateResponse() { State = state.Value };
                return Json(result);
            }
            else
            {
                return StatusCode(404);
            }
        }

        #endregion

        #region GET {host}/GetDeliveryMethods - Получение вариантов доставки

        [HttpGet("/GetDeliveryMethods")]
        public IActionResult GetDeliveryMethods()
        {
            var methods = _deliveryMethodRepository.GetAll();
            var result = new GetDeliveryMethodsResponse() { methods = methods };

            return Json(result);
        }

        #endregion

        #region GET {host}/Admin/Orders?page={page_numer}&status[]={status1}&status[]={status2} - Просмотр списка заказов (с пагинацией и фильтрацией по нескольким статусам)

        [Authorize]
        [HttpGet("/Admin/Orders")]
        public IActionResult GetOrders(int page, OrderState[] states)
        {
            var limit = 25;
            var skip = (page - 1) * 25;
            var orders = _orderRepository.GetAll(skip, limit, states);
            var ordersCount = _orderRepository.Count(states);

            var result = new ShortOrdersResponse()
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)ordersCount / limit)
            };

            return Json(result);
        }

        #endregion

        #region GET {host}/Admin/Orders/{orderId} - Просмотр конкретного заказа

        [Authorize]
        [HttpGet("/Admin/Orders/{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                return Json(order);
            }
            else
            {
                return StatusCode(404);
            }
        }

        #endregion

        #region PUT {host}/Admin/Orders/{orderId} - Редактирование конкретного заказа

        [Authorize]
        [HttpPut("/Admin/Orders/{orderId}")]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderDetails order)
        {
            if (order.Id != orderId)
                return StatusCode(400);

            var id = _orderRepository.Update(order);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion



        #region GET {host}/Admin/Orders/{orderId}/Items - Просмотр товаров конкретного заказа

        [Authorize]
        [HttpGet("/Admin/Orders/{orderId}/Items")]
        public IActionResult GetOrderItems(int orderId)
        {
            var orderItems = _orderRepository.GetOrderItems(orderId);
            var result = new OrderItemsResponse()
            {
                Items = orderItems
            };

            return Json(result);
        }

        #endregion

        #region POST {host}/Admin/Orders/{orderId}/Items - Добавление товаров в конкретный заказ

        [Authorize]
        [HttpPost("/Admin/Orders/{orderId}/Items")]
        public IActionResult AddOrderItem(int orderId, [FromBody] OrderItem orderItem)
        {
            if (orderItem.OrderId != orderId)
                return StatusCode(400);

            var id = _orderRepository.AddOrderItem(orderItem);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion

        #region PUT {host}/Admin/Orders/{orderId}/Items/{orderItemId} - Редактирование товара в конкретном заказе

        [Authorize]
        [HttpPut("/Admin/Orders/{orderId}/Items/{orderItemId}")]
        public IActionResult UpdateOrderItem(int orderId, int orderItemId, [FromBody] OrderItem orderItem)
        {
            var id = _orderRepository.UpdateOrderItem(orderItem);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion

        #region DELETE {host}/Admin/Orders/{orderId}/Items/{orderItemId} - Удаление товара в конкретном заказе

        [Authorize]
        [HttpDelete("/Admin/Orders/{orderId}/Items/{orderItemId}")]
        public IActionResult DeleteOrderItem(int orderId, int orderItemId)
        {
            var id = _orderRepository.DeleteOrderItem(orderId, orderItemId);
            if (id != null)
                return StatusCode(200);
            else
                return StatusCode(404);
        }

        #endregion
    }
}
