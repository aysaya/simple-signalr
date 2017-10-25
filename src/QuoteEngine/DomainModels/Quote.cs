using Newtonsoft.Json;
using System;

namespace QuoteEngine.DomainModels
{
    public class Quote
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
