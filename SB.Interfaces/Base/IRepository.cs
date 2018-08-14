using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SB.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> List(int page, int pageSize);
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate, int page, int pageSize);
        void Add(T entity);
        Task Delete(Guid id);
        void Update(T entity);
    }
}
