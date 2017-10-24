using Infrastructure.ServiceBus;
using QuoteEngine.DomainModels;
using QuoteEngine.ResourceAccessors;
using System.Threading.Tasks;

namespace QuoteEngine.MessageHandlers
{
    public class ThirdPartyRateProcessor : IProcessMessage<Contracts.CreateQuote>
    {
        private readonly ICommandRA<Quote> commandRA;
        private readonly IPublishMessage<Contracts.NewQuoteReceived> messagePublisher;

        public ThirdPartyRateProcessor(ICommandRA<Quote> commandRA, IPublishMessage<Contracts.NewQuoteReceived> messagePublisher)
        {
            this.commandRA = commandRA;
            this.messagePublisher = messagePublisher;
        }

        public async Task ProcessAsync(Contracts.CreateQuote message)
        {
            var quote = await commandRA.SaveAsync(AssembleQuote(message));

            var pubTask = messagePublisher.SendAsync(PrepareEventMessage(quote));            
        }


        private Quote AssembleQuote(Contracts.CreateQuote payload)
        {
            return new Quote
            {
                BaseCurrency = payload.BaseCurrency,
                TargetCurrency = payload.TradeCurrency,
                Rate = payload.Rate
            };
        }

        private Contracts.NewQuoteReceived PrepareEventMessage(Quote quote)
        {
            return new Contracts.NewQuoteReceived
            {
                Id = quote.Id,
                BaseCurrency = quote.BaseCurrency,
                TradeCurrency = quote.TargetCurrency,
                Rate = quote.Rate
            };
        }
    }
}
