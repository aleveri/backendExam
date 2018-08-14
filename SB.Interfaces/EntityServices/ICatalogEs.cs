using SB.Entities;
using System.Threading.Tasks;

namespace SB.Interfaces
{
    public interface ICatalogEs : IBaseService<Catalog>
    {
        Task<IResponseService> ListByType(Pagination param);
        Task<IResponseService> ListByParent(Pagination param);
    }
}
