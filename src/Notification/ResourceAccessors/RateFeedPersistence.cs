using Infrastructure.CosmosDb;
using Notification.DomainModels;
using System;
using System.Threading.Tasks;

namespace Notification.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T t);
    }
    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class RateFeedPersistence : ICommandRA<RateFeed>, IQueryRA<RateFeed>
    {
        private readonly IProvideDocumentRepository<RateFeed> repository;

        public RateFeedPersistence(IProvideDocumentRepository<RateFeed> repository)
        {
            this.repository = repository;
        }

        public async Task<RateFeed> SaveAsync(RateFeed rateFeed)
        {
            rateFeed.DateCreated = DateTime.UtcNow;

            return await repository.SaveAsync(rateFeed);
        }

        public async Task<RateFeed[]> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }        
    }
}
