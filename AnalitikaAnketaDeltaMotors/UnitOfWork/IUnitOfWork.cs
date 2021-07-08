using System;
using System.Threading.Tasks;

namespace UnitOfWorkExample.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository Repository();
        Task CommitAsync();
        int Commit();
    }
}