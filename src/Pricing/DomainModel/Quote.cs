using Newtonsoft.Json;
using System;

namespace Pricing.DomainModel
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
