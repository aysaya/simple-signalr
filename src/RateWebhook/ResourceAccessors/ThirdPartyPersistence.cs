using Infrastructure.Common;
using RateWebhook.DomainModels;
using System;
using System.Threading.Tasks;

namespace RateWebhook.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T rate);
        Task DeleteAllAsync();
    }

    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class ThirdPartyPersistence : ICommandRA<ThirdPartyRate>, IQueryRA<ThirdPartyRate>
    {
        private readonly IProvideRepository<ThirdPartyRate> thirdPartyRepository;

        public ThirdPartyPersistence(IProvideRepository<ThirdPartyRate> thirdPartyRepository)
        {
            this.thirdPartyRepository = thirdPartyRepository;
        }

        public async Task<ThirdPartyRate> SaveAsync(ThirdPartyRate rate)
        {
            rate.DateCreated = DateTime.UtcNow;

            return await thirdPartyRepository.SaveAsync(rate);
        }

        public async Task<ThirdPartyRate[]> GetAllAsync()
        {
            return await thirdPartyRepository.GetAllAsync();
        }

        public async Task DeleteAllAsync()
        {
            await thirdPartyRepository.DeleteAllAsync();
        }
    }
}
