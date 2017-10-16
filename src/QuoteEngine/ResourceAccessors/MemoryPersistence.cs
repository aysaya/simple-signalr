using QuoteEngine.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteEngine.ResourceAccessors
{
    public interface ICommandRA
    {
        Task SaveAsync(Quote quote);
    }
    public interface IQueryRA
    {
        Task<Quote[]> GetAllAsync();
    }

    public class MemoryPersistence : ICommandRA, IQueryRA
    {
        private static HashSet<Quote> cache = new HashSet<Quote>();
        
        public async Task SaveAsync(Quote quote)
        {
            cache.Add(quote);
            await Task.CompletedTask;
        }

        public async Task<Quote[]> GetAllAsync()
        {
            return await Task.FromResult(cache.ToArray());
        }        
    }
}
