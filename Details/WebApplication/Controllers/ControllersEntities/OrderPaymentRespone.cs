namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода /CheckPayment?order= - Проверка оплаты конкретного заказа
    /// </summary>
    public class OrderPaymentRespone
    {
        public int Status { get; set; } // think about it: enum
    }
}
