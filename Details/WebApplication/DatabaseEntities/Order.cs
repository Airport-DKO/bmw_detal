using System;

namespace WebApplication.Controllers.ControllersEntities
{
    public enum OrderState
    {
        New = 0,
        // think about it: статусы заказа
        Canceled = 10
    }

    public enum PaymentType
    {
        Cash = 0, // think about it: способы оплаты
        NonCash
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
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    /// <summary>
    /// Список элементов заказа: деталь + количество
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
