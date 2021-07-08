using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitOfWorkExample.UnitOfWork
{
    public interface IGenericRepository
    {
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class;
        IEnumerable<T> All<T>() where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> expression) where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}
