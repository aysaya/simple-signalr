using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.ServiceBus;
using RateWebhook.ResourceAccessors;
using RateWebhook.DomainModels;

namespace RateWebhook.Controllers
{
    [Route("api/[controller]")]
    public class ThirdpartyRatesController : Controller
    {
        private readonly ISendMessage<Contracts.CreateQuote> sender;
        private readonly IQueryRA<ThirdPartyRate> query;
        private readonly ICommandRA<ThirdPartyRate> command;

        public ThirdpartyRatesController(ISendMessage<Contracts.CreateQuote> sender, 
            IQueryRA<ThirdPartyRate> query, ICommandRA<ThirdPartyRate> command)
        {
            this.sender = sender;
            this.command = command;
            this.query = query;
        }

        [HttpPost]
        public async Task Post([FromBody]Contracts.ThirdPartyRate value)
        {
            var thirdPartyRate = await command.SaveAsync
                (
                    new ThirdPartyRate
                    {
                        BaseCurrency = value.BaseCurrency,
                        TradeCurrency = value.TradeCurrency,
                        Rate = value.Rate,
                        ReferenceId = value.PartnerId
                    }
                );

            var taskSend = sender.SendAsync(new Contracts.CreateQuote
            {
                Id = thirdPartyRate.Id,
                BaseCurrency = thirdPartyRate.BaseCurrency,
                TradeCurrency = thirdPartyRate.TradeCurrency,
                Rate = thirdPartyRate.Rate,
            });

            await Task.CompletedTask;
        }

        [HttpGet]
        public async Task<ThirdPartyRate[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
