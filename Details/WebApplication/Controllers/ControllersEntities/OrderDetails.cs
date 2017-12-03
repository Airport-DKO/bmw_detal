using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerComment { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public decimal DeliveryPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderState State { get; set; }
    }
}
