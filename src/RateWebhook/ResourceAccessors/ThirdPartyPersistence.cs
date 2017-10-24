using Infrastructure.CosmosDb;
using RateWebhook.DomainModels;
using System.Threading.Tasks;

namespace RateWebhook.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T rate);
    }

    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class ThirdPartyPersistence : ICommandRA<ThirdPartyRate>, IQueryRA<ThirdPartyRate>
    {
        private readonly IProvideDocumentRepository<ThirdPartyRate> thirdPartyRepository;

        public ThirdPartyPersistence(IProvideDocumentRepository<ThirdPartyRate> thirdPartyRepository)
        {
            this.thirdPartyRepository = thirdPartyRepository;
        }

        public async Task<ThirdPartyRate> SaveAsync(ThirdPartyRate rate)
        {
            return await thirdPartyRepository.SaveAsync(rate);            
        }

        public async Task<ThirdPartyRate[]> GetAllAsync()
        {
            return await thirdPartyRepository.GetAllAsync();
        }
    }
}
