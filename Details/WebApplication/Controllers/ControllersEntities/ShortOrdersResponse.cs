
namespace WebApplication.Controllers.ControllersEntities
{
    public class ShortOrdersResponse
    {
        public ShortOrder[] Orders { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class ShortOrder
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public decimal Sum { get; set; }
        public OrderState State { get; set; }
    }
}
