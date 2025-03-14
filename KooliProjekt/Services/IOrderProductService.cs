using KooliProjekt.Data;
using KooliProjekt.Search;

public interface IOrderProductService
{
    Task<PagedResult<OrderProduct>> List(int page, int pageSize, OrderSearch query = null);
    Task<OrderProduct> Get(int id);
    Task Save(OrderProduct orderProduct);
    Task Delete(int id);
}
