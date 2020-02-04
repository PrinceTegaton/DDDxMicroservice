using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Repository
{
    public interface ISimpleRepository<TEntity> where TEntity : class
    {
        void DetachAllEntities();
        IEnumerable<TEntity> SqlQuery<TEntity>(string sqlQuery, params object[] parameters) where TEntity : class, new();
        void BeginTransaction();
        Task<bool> CommitTransactionAsync();
        bool CommitTransaction();
        void RollbackTransaction();
        Task<bool> SaveAsync();
        bool Save();
        bool Remove(TEntity entity);
        bool Remove(object id);
        bool AddRange(IEnumerable<TEntity> entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entity);
        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        bool Delete(object id);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter);
        bool Delete(Expression<Func<TEntity, bool>> filter);
        Task<bool> DeleteAsync(object id);
        int Count();
        int Count(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
    }
}