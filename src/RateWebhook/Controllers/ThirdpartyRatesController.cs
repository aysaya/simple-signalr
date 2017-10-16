using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RateWebhook.Models;
using RateWebhook.ResourceAccessors;

namespace RateWebhook.Controllers
{
    [Route("api/[controller]")]
    public class ThirdpartyRatesController : Controller
    {
        private readonly ISendMessage sender;
        private readonly IQueryRA query;
        private readonly ICommandRA command;

        public ThirdpartyRatesController(ISendMessage sender, IQueryRA query, ICommandRA command)
        {
            this.sender = sender;
            this.command = command;
            this.query = query;
        }

        [HttpPost]
        public async Task Post([FromBody]ThirdPartyRate value)
        {
            var saveTask = command.SaveAsync(value);
            await sender.Send(value);
        }

        [HttpGet]
        public async Task<ThirdPartyRate[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
