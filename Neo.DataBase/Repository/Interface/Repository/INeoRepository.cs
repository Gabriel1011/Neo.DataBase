using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.DataBaseRepository.Interface.Repository
{
    public interface INeoRepository<T> where T : NeoEntity
    {
        public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
