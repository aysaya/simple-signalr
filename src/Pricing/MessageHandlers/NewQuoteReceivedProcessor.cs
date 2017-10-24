using Contracts;
using Infrastructure.ServiceBus;
using Pricing.DomainModel;
using Pricing.ResourceAccessors;
using System.Threading.Tasks;

namespace Pricing.MessageHandlers
{

    public class NewQuoteReceivedProcessor : IProcessMessage<NewQuoteReceived>
    {
        private readonly ICommandRA<Quote> commandRA;

        public NewQuoteReceivedProcessor(ICommandRA<Quote> commandRA)
        {
            this.commandRA = commandRA;
        }

        public async Task ProcessAsync(NewQuoteReceived message)
        {
            await commandRA.SaveAsync
                ( new Quote
                    {
                        Id = message.Id,
                        BaseCurrency = message.BaseCurrency,
                        TargetCurrency = message.TradeCurrency,
                        Rate = message.Rate
                    }
                );
        }

    }
}
