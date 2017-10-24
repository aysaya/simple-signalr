using Microsoft.AspNetCore.Mvc;
using Pricing.DomainModel;
using Pricing.ResourceAccessors;
using System.Threading.Tasks;

namespace Pricing.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQueryRA<Quote> query;

        public QuotesController(IQueryRA<Quote> query)
        {
            this.query = query;
        }

        [HttpGet]
        public async Task<Quote[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
