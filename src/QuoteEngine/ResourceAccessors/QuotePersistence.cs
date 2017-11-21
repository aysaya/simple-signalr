using Infrastructure.Common;
using QuoteEngine.DomainModels;
using System;
using System.Threading.Tasks;

namespace QuoteEngine.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T quote);
    }
    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class QuotePersistence : ICommandRA<Quote>, IQueryRA<Quote>
    {
        private readonly IProvideRepository<Quote> quoteRepository;

        public QuotePersistence(IProvideRepository<Quote> quoteRepository)
        {
            this.quoteRepository = quoteRepository;
        }

        public async Task<Quote> SaveAsync(Quote quote)
        {
            quote.DateCreated = DateTime.UtcNow;

            return await quoteRepository.SaveAsync(quote);
        }

        public async Task<Quote[]> GetAllAsync()
        {
            return await quoteRepository.GetAllAsync();                
        }
    }
}
