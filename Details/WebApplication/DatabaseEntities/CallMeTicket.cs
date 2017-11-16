namespace WebApplication.DatabaseEntities
{
    public enum CallMeTicketType
    {
        Call = 0,
        Telegram,
        Viber,
        WhatsUp
    }
    
    /// <summary>
    /// Запрос "свяжитесь со мной"
    /// </summary>
    public class CallMeTicket
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public CallMeTicketType Type { get; set; }
        public bool IsNew { get; set; }
    }
}
