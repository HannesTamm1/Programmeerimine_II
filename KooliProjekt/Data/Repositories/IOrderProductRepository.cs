namespace KooliProjekt.Data.Repositories
{
    public interface IOrderProductRepository
    {
        Task<OrderProduct> Get(int id);
        Task<PagedResult<OrderProduct>> List(int page, int pageSize);
        Task Save(OrderProduct orderProduct);
        Task Delete(int id);
    }
}
