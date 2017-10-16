using Microsoft.AspNetCore.Mvc;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        private readonly IQueryRA query;

        public NotificationsController(IQueryRA query)
        {
            this.query = query;
        }

        [HttpGet]
        public async Task<Models.Notification[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
