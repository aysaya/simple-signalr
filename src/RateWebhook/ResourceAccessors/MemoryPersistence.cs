using Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateWebhook.ResourceAccessors
{
    public interface ICommandRA
    {
        Task SaveAsync(ThirdPartyRate rate);
    }
    public interface IQueryRA
    {
        Task<ThirdPartyRate[]> GetAllAsync();
    }

    public class MemoryPersistence : ICommandRA, IQueryRA
    {
        private static HashSet<ThirdPartyRate> cache = new HashSet<ThirdPartyRate>();
        
        public async Task SaveAsync(ThirdPartyRate rate)
        {
            cache.Add(rate);
            await Task.CompletedTask;
        }

        public async Task<ThirdPartyRate[]> GetAllAsync()
        {
            return await Task.FromResult(cache.ToArray());
        }        
    }
}
