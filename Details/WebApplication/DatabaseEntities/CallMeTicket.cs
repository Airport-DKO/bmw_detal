namespace WebApplication.DatabaseEntities
{
    public enum CallMeTicketType
    {
        Call = 0,
        Telegram,
        Viber,
        WhatsUp
    }

    public enum CallMeTicketState
    {
        New = 0,
        Done
    }

    /// <summary>
    /// Запрос "свяжитесь со мной"
    /// </summary>
    public class CallMeTicket
    {
        public string MobileNumber { get; set; }
        public CallMeTicketType Type { get; set; }
        public CallMeTicketState State { get; set; }
    }
}
