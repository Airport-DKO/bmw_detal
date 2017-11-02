namespace WebApplication.Controllers.ControllersEntities
{
    /// <summary>
    /// Структура ответа для api-метода {host}/?q= - Быстрый поиск
    /// </summary>
    public class QuickSearchResponse
    {
        public QuickSearchResult[] Details { get; set; }
    }

    public class QuickSearchResult
    {
        public string DetailNumber { get; set; }
        public string DetailName { get; set; }

    }
}
