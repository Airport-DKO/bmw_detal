namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода /SelectPaymentMethod - Выбор типа оплаты конкретного заказа
    /// </summary>
    public class SelectPaymentMethodResponse
    {
        public string LinkToPayment { get; set; }
    }
}
