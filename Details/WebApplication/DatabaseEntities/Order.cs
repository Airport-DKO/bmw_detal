namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура заказа
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public OrderItem[] Items { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerComment { get; set; }
        public int DeliveryType { get; set; } // think about it: договоримся о списке позже -- enum?
        // think about it: статус заказа
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
