namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура запроса для api-метода /СheckOrderAvailability - Проверка возможности оформления заказа
    /// </summary>
    public class CheckOrderAvailabilityRequest
    {
        public OrderItem[] Items { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerComment { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
