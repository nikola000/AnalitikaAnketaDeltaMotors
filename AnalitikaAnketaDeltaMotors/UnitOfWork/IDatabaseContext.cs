using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace UnitOfWorkExample.UnitOfWork
{
    public interface IDatabaseContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(System.Type entityType);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}