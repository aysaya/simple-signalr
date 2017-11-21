using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common
{
    public interface IStorageDbOptionsBuilder
    {
        IServiceCollection Services { get; }
    }

    public class StorageDbOptionsBuilder : IStorageDbOptionsBuilder
    {
        public StorageDbOptionsBuilder(IServiceCollection services)
        {
            Services = services;
        }
        public IServiceCollection Services { get; }
    }
}
