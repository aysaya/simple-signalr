using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sqlite
{
    public interface ISqliteDbContext<T> where T: class
    {
        DbSet<T> TEntity { get; set; }
    }

    public class SqliteDbContext<T> : DbContext, ISqliteDbContext<T> where T : class
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext<T>> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<T>(entity => {
                entity.ToTable(typeof(T).Name);
            });
        }

        public DbSet<T> TEntity { get; set; }
    }
}
