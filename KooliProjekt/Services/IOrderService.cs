using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IOrderService
    {
        Task<PagedResult<Order>> List(int page, int pageSize);
        Task<Order> Get(int id);
        Task Save(Order order);
        Task Delete(int id);
    }
}
