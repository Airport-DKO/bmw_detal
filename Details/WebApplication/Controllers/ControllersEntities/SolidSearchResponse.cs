using WebApplication.DatabaseEntities;

namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода {host}solid/?q= - Полный поиск
    /// </summary>
    public class SolidSearchResponse
    {
        public SolidSearchResult[] Details { get; set; }
    }

    public class SolidSearchResult
    {
        public int InternalId { get; set; }
        public DetailType Type { get; set; }
        public string DetailNumber { get; set; }
        public string OriginalDetailNumber { get; set; }
        public string DetailName { get; set; }
        public decimal Price { get; set; }
        public string Delivery { get; set; } // think about it: способ доставки - это по сути что?
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
