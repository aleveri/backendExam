using Microsoft.EntityFrameworkCore;
using SB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SB.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public void Add(T entity) => _unitOfWork.Context.Set<T>().Add(entity);

        public void Update(T entity) => _unitOfWork.Context.Entry(entity).State = EntityState.Modified;

        public async Task<T> Get(Guid id) => await _unitOfWork.Context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> List(int page, int pageSize) => await _unitOfWork.Context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate, int page, int pageSize) => await _unitOfWork.Context.Set<T>().Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task Delete(Guid id)
        {
            T existing = await _unitOfWork.Context.Set<T>().FindAsync(id);
            if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return await query.AnyAsync(where); ;
        }

        public IQueryable<T> AsQueryable() => _unitOfWork.Context.Set<T>().AsQueryable();

        private static IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query) => includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
