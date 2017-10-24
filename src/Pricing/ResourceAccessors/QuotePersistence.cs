using Infrastructure.CosmosDb;
using Pricing.DomainModel;
using System.Threading.Tasks;

namespace Pricing.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T t);
    }
    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class QuotePersistence : ICommandRA<Quote>, IQueryRA<Quote>
    {
        private readonly IProvideDocumentRepository<Quote> repository;

        public QuotePersistence(IProvideDocumentRepository<Quote> repository)
        {
            this.repository = repository;
        }

        public async Task<Quote> SaveAsync(Quote quote)
        {
            return await repository.SaveAsync(quote);
        }

        public async Task<Quote[]> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }        
    }
}
