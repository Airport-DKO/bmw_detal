namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура запроса для api-метода /SelectPaymentMethod - Выбор типа оплаты конкретного заказа
    /// </summary>
    public class SelectPaymentMethodRequest
    {
        public int OrderId { get; set; }
        public int PaymentType { get; set; }
    }
}
