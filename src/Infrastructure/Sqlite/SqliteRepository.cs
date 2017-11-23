using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Sqlite
{
    public class SqliteRepository<T> : IProvideRepository<T> where T: class
    {
        private readonly ISqliteDbContext<T> context;

        public SqliteRepository(ISqliteDbContext<T> context)
        {
            this.context = context;
        }

        public async Task DeleteAllAsync()
        {
            context.TEntity.RemoveRange(context.TEntity);
            ((DbContext)context).SaveChanges();

            await Task.CompletedTask;
        }

        public async Task<T[]> GetAllAsync()
        {
            var result = await context.TEntity.ToArrayAsync();

            return result;
        }

        public async Task<T> SaveAsync(T t)
        {
            var result = await context.TEntity.AddAsync(t);
            ((DbContext)context).SaveChanges();

            return result.Entity;
        }
    }
}
