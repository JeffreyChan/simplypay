using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimplePayment.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        T AddOrUpdate(T entity);
        Task<IEnumerable<T>> FindTopN(Expression<Func<T, bool>> predicate, int topN, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T>
       where T : BaseEntity
    {
        protected SimplePaymentContext DbContext;
        private readonly IDbSet<T> _dbSet;

        protected GenericRepository(SimplePaymentContext context)
        {
            DbContext = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> IncludeExpressions(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(query, (current, expression) => current.Include(expression));
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = IncludeExpressions(_dbSet, includes);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = IncludeExpressions(_dbSet, includes);

            var result = await query.Where(predicate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = IncludeExpressions(_dbSet, includes);

            var result = await query.Where(predicate).ToListAsync();
            return result;
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).CountAsync();
            return result;
        }

        public T AddOrUpdate(T entity)
        {
            var tracked = _dbSet.Find(DbContext.KeyValuesFor(entity));
            if (tracked != null)
            {
                DbContext.Entry(tracked).CurrentValues.SetValues(entity);
                return tracked;
            }

            _dbSet.Add(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> FindTopN(Expression<Func<T, bool>> predicate, int topN, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = _dbSet.Include(include);
            }

            query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var result = await query.Take(topN).ToListAsync();
            return result;
        }
    }
}
