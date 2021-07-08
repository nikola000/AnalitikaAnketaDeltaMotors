using System.Threading.Tasks;

namespace UnitOfWorkExample.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _databaseContext;

        public UnitOfWork(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IGenericRepository Repository()
        {
            return new GenericRepository(_databaseContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }

        public Task CommitAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        public int Commit()
        {
            return _databaseContext.SaveChanges();
        }
    }
}