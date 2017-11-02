﻿namespace WebApplication.DatabaseEntities
{
    public enum DetailType
    {
        Origin = 0,
        Analog
    }

    public class Detail
    {
        public int InternalId { get; set; }
        public DetailType Type { get; set; }
        public string DetailNumber { get; set; }
        public string OriginalDetailNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Delivery { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}