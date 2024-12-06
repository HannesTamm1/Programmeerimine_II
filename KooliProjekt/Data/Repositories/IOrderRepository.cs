namespace KooliProjekt.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Get(int id);
        Task<PagedResult<Order>> List(int page, int pageSize);
        Task Save(Order order);
        Task Delete(int id);
    }
}
