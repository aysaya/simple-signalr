using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using QuoteEngine.DomainModels;
using QuoteEngine.ResourceAccessors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteEngine.MessageHandlers
{
    public interface IProcessMessage
    {
        Task ProcessAsync(Message message);
    }
    public class MessageProcessor : IProcessMessage
    {
        private readonly ICommandRA commandRA;
        private readonly IPublishMessage messagePublisher;

        public MessageProcessor(ICommandRA commandRA, IPublishMessage messagePublisher)
        {
            this.commandRA = commandRA;
            this.messagePublisher = messagePublisher;
        }

        public async Task ProcessAsync(Message message)
        {
            var quote = AssembleQuote(Encoding.UTF8.GetString(message.Body));

            var saveTask = commandRA.SaveAsync(quote);

            await messagePublisher.Publish(PrepareEventMessage(quote));
        }

        private Quote AssembleQuote(string payload)
        {
            var keyValuePairs = JsonConvert
                .DeserializeObject<Dictionary<string, string>>(payload);

            return new Quote
            {
                Id = Guid.NewGuid(),
                BaseCurrency = keyValuePairs["BaseCurrency"],
                TargetCurrency = keyValuePairs["TradeCurrency"],
                Rate = Convert.ToDouble(keyValuePairs["Rate"])
            };
        }

        private string PrepareEventMessage(Quote quote)
        {
            return JsonConvert.SerializeObject(quote);
        }
    }
}
