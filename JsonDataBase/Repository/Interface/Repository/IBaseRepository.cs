using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neo.DataBaseRepository.Interface.Repository
{
    public interface IBaseRepository<T> where T : NeoEntity
    {
        public Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter);
        public Task<T> Get(Expression<Func<T, bool>> filter);
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task Delete(T entity);
    }
}
