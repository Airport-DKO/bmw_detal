namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода /CheckPayment?order= - Проверка оплаты конкретного заказа
    /// </summary>
    public class OrderStateResponse
    {
        public OrderState State { get; set; }
    }
}
