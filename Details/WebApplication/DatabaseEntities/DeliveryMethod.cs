using System;

namespace WebApplication.DatabaseEntities
{
    public enum DeliveryLocationType
    {
        Moscow = 0,
        Russia
    }

    public enum DeliveryType
    {
        Lightweight = 0,
        Largeweight
    }

    public class DeliveryMethod
    {
        public int Id { get; set; }
        public DeliveryLocationType LocationType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string DeliveryName { get; set; }
        public Decimal? Price { get; set; } // если 0 - доставка бесплатная, если поле отсутствует - обсуждается индивидуально

    }
}
