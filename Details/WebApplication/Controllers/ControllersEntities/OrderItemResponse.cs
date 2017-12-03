using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    public class OrderItemsResponse
    {
        public OrderItemDetails[] Items { get; set; }
    }

    public class OrderItemDetails
    {
        public int OrderItemId { get; set; }
        public int InternalId { get; set; }
        public DetailType Type { get; set; }
        public string DetailNumber { get; set; }
        public string OriginalDetailNumber { get; set; }
        public string DetailName { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTimeInfo { get; set; }
        public int quantity { get; set; }
    }
}
