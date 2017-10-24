using Newtonsoft.Json;
using System;

namespace RateWebhook.DomainModels
{
    public class ThirdPartyRate
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DateCreated => DateTime.UtcNow;        
    }
}
