using System;

namespace QuoteEngine.DomainModels
{
    public class Quote
    {
        public Guid Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double Rate { get; set; }
    }
}
