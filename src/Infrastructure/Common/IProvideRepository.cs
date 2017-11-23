using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public interface IProvideRepository<T>
    {
        Task<T> SaveAsync(T t);

        Task<T[]> GetAllAsync();

        Task DeleteAllAsync();
    }
}
