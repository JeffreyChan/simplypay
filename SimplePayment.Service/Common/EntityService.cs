using SimplePayment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimplePayment.Service
{
    public interface IService
    {
    }

    public interface IEntityService<T> : IService
     where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> Delete(T entity);
        Task<T> Update(T entity);
        Task<T> AddOrUpdate(T entity);
        Task<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindTopN(Expression<Func<T, bool>> predicate, int topN, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
    }

    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IGenericRepository<T> Repository;

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }


        public async Task<T> Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Repository.Add(entity);
            await UnitOfWork.CommitAsync();
            return entity;
        }


        public async Task<T> Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Repository.Edit(entity);
            await UnitOfWork.CommitAsync();
            return entity;
        }

        public async Task<T> AddOrUpdate(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Repository.AddOrUpdate(entity);
            await UnitOfWork.CommitAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Repository.Delete(entity);
            await UnitOfWork.CommitAsync();

            return entity;
        }

        public Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return Repository.GetAll(includes);
        }

        public Task<IEnumerable<T>> FindTopN(Expression<Func<T, bool>> predicate, int topN, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            return Repository.FindTopN(predicate, topN, orderBy, includes);
        }

        public Task<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Repository.Get(predicate, includes);
        }

        public Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Repository.FindBy(predicate, includes);
        }


        public Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return Repository.Count(predicate);
        }
    }
}
