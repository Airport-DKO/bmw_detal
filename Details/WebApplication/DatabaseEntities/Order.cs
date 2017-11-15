namespace WebApplication.Controllers.ControllersEntities
{
    public enum OrderState
    {
        New = 0, // think about it: статусы заказа
    }

    /// <summary>
    /// Структура заказа
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public OrderItem[] Items { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerComment { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderState State { get; set; }
    }

    /// <summary>
    /// Список элементов заказа: деталь + количество
    /// </summary>
    public class OrderItem
    {
        public int InternalId { get; set; }
        public int Quantity { get; set; }
    }
}
