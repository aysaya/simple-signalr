namespace Contracts
{
    public class CreateQuote
    {
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TradeCurrency { get; set; }
        public double Rate { get; set; }
    }
}
