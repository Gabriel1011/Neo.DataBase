using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.DataBaseRepository.Interface.Repository
{
    public interface INeoRepository<TEntity> where TEntity : NeoEntity
    {
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter);
        public TEntity Get(Expression<Func<TEntity, bool>> filter);
        public TEntity Create(TEntity entity);
        public IEnumerable<TEntity> CreateList(IEnumerable<TEntity> entities);
        public TEntity Update(TEntity entity);
        public IEnumerable<TEntity> UpdateList(IEnumerable<TEntity> entities);
        public void Delete(TEntity entity);
        public void DeleteList(IEnumerable<TEntity> entities);

        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> entity);
        public Task DeleteAsync(TEntity entity);
        public Task DeleteListAsync(IEnumerable<TEntity> entity);
    }
}
