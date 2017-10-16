using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.ResourceAccessors
{
    public interface ICommandRA
    {
        Task SaveAsync(Models.Notification notification);
    }
    public interface IQueryRA
    {
        Task<Models.Notification[]> GetAllAsync();
    }

    public class MemoryPersistence : ICommandRA, IQueryRA
    {
        private static HashSet<Models.Notification> cache = new HashSet<Models.Notification>();
        
        public async Task SaveAsync(Models.Notification notification)
        {
            cache.Add(notification);
            await Task.CompletedTask;
        }

        public async Task<Models.Notification[]> GetAllAsync()
        {
            return await Task.FromResult(cache.ToArray());
        }        
    }
}
