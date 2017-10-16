using Microsoft.AspNetCore.Mvc;
using Pricing.ResourceAccessors;
using System.Threading.Tasks;

namespace Pricing.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQueryRA query;

        public QuotesController(IQueryRA query)
        {
            this.query = query;
        }

        [HttpGet]
        public async Task<DomainModel.Quote[]> Get()
        {
            return await query.GetAllAsync();
        }
    }
}
