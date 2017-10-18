using System;

namespace Contracts
{
    public class NewQuoteReceived
    {
        public Guid Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
    }
}
