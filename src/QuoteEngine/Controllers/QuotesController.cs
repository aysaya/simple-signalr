using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuoteEngine.ResourceAccessors;
using QuoteEngine.DomainModels;

namespace QuoteEngine.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQueryRA queryRa;
        public QuotesController(IQueryRA queryRa)
        {
            this.queryRa = queryRa;
        }

        [HttpGet]
        public async Task<Quote[]> Get()
        {
            return await queryRa.GetAllAsync();
        }        
    }
}
