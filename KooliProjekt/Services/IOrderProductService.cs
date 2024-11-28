using KooliProjekt.Data;

public interface IOrderProductService
{
    Task<PagedResult<OrderProduct>> List(int page, int pageSize);
    Task<OrderProduct> Get(int id);
    Task Save(OrderProduct orderProduct);
    Task Delete(int id);
}
