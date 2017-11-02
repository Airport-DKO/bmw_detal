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
        public string MobileNumber { get; set; }
        public CallMeTicketType Type { get; set; }
        // think about it: нужен статус? тогда нужно будет сделать dto для метода CallMeTicket
    }
}
