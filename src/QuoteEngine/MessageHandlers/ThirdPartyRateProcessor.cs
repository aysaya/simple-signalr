using Contracts;
using Infrastructure.ServiceBus;
using QuoteEngine.DomainModels;
using QuoteEngine.ResourceAccessors;
using System;
using System.Threading.Tasks;

namespace QuoteEngine.MessageHandlers
{
    public class ThirdPartyRateProcessor : IProcessMessage
    {
        private readonly ICommandRA commandRA;
        private readonly IPublishMessage messagePublisher;

        public ThirdPartyRateProcessor(ICommandRA commandRA, IPublishMessage messagePublisher)
        {
            this.commandRA = commandRA;
            this.messagePublisher = messagePublisher;
        }

        public async Task ProcessAsync<T>(T message)
        {
            var quote = AssembleQuote(message as ThirdPartyRate);

            var saveTask = commandRA.SaveAsync(quote);

            await messagePublisher.PublishAsync(PrepareEventMessage(quote));
        }


        private Quote AssembleQuote(ThirdPartyRate payload)
        {
            return new Quote
            {
                Id = Guid.NewGuid(),
                BaseCurrency = payload.BaseCurrency,
                TargetCurrency = payload.TradeCurrency,
                Rate = payload.Rate
            };
        }

        private NewQuoteReceived PrepareEventMessage(Quote quote)
        {
            return new NewQuoteReceived
            {
                Id = quote.Id,
                BaseCurrency = quote.BaseCurrency,
                TradeCurrency = quote.TargetCurrency,
                Rate = quote.Rate
            };
        }
    }
}
