using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitOfWorkExample.UnitOfWork
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDatabaseContext _dbContext;

        public GenericRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class 
        {
            return _dbContext.Set<T>().Where(expression).ToList();
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _dbContext.Set<T>().SingleOrDefault(expression);
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> All<T>() where T : class
        {
            return _dbContext.Set<T>().AsQueryable().ToList();
        }
    }
}