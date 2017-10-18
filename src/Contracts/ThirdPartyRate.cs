namespace Contracts
{
    public class ThirdPartyRate
    {
        public string PartnerId { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
    }
}
