namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода /СheckOrderAvailability - Проверка возможности оформления заказа
    /// </summary>
    public class CheckOrderAvailabilityResponse
    {
        public int? OrderId { get; set; }
        public OrderItem[] ProblemItems { get; set; }
    }
}
