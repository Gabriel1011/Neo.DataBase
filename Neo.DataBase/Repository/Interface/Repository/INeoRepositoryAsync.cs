using Neo.DataBaseRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.DataBase.Repository.Interface.Repository
{
    public interface INeoRepositoryAsync<TEntity> where TEntity : NeoEntity
    {
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
