﻿namespace Notification.Models
{
    public class RateFeed
    {
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
    }
}
