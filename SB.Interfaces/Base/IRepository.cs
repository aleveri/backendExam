using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SB.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> List(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> filter = null, int page = 1, int pageSize = 20,
             params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        Task Delete(Guid id);
        void Update(T entity);
    }
}
