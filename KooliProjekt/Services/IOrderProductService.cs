using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IOrderProductService
    {
        Task<PagedResult<OrderProduct>> List(int page, int pageSize, OrderProductsSearch query = null);
        Task<OrderProduct> Get(int id);
        Task Save(OrderProduct orderProduct);
        Task Delete(int id);
    }
}
