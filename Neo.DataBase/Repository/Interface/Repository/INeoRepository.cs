using Neo.DataBaseRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Neo.DataBase.Repository.Interface.Repository
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

    }
}
