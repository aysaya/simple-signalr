﻿namespace Contracts
{
    public class NewQuoteReceived
    {
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
    }
}
