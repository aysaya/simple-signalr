using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.Hubs
{
    public interface INotifyRateFeedClient
    {
        Task Notify(string payload);
    }
    public class RateFeedClientNotifier : INotifyRateFeedClient
    {
        private readonly IProvideRateFeedClientContext rateFeedClientContext;

        public RateFeedClientNotifier(IProvideRateFeedClientContext rateFeedClientContext)
        {
            this.rateFeedClientContext = rateFeedClientContext;
        }

        public Task Notify(string payload)
        {
            var hub = new RateFeedHub
            {
                Clients = rateFeedClientContext.RateFeedClients.Clients
            };
            hub.Send(AssembleRateFeedData(payload));
            return Task.CompletedTask;
        }

        private RateFeedData AssembleRateFeedData(string payload)
        {
            var keyValuePairs = JsonConvert
                .DeserializeObject<Dictionary<string, string>>(payload);

            return new RateFeedData
            {
                BaseCurrency = keyValuePairs["BaseCurrency"],
                TargetCurrency = keyValuePairs["TradeCurrency"],
                RateValue = Convert.ToDouble(keyValuePairs["Rate"])
            };
        }
    }
}
