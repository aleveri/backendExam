using SB.Entities;
using System;
using System.Threading.Tasks;

namespace SB.Interfaces
{
    public interface IBaseService<in T>
    {
        Task<IResponseService> Add(T entity);
        Task<IResponseService> Get(Guid id);
        Task<IResponseService> Update(T entity);
        Task<IResponseService> Delete(Guid id);
        Task<IResponseService> List(Pagination param);
        Task<IResponseService> ListAsync(Pagination param);
    }
}
