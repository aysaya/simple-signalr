using Microsoft.AspNetCore.Mvc;
using Notification.Models;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    public class RateFeedController : Controller
    {
        private readonly IQueryRA<RateFeed> query;

        public RateFeedController(IQueryRA<RateFeed> query)
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
