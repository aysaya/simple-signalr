using Microsoft.AspNetCore.Mvc;
using Notification.DomainModels;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    public class RateFeedsController : Controller
    {
        private readonly IQueryRA<RateFeed> query;

        public RateFeedsController(IQueryRA<RateFeed> query)
        {
            this.query = query;
        }

        [HttpGet]
        public async Task<RateFeed[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
