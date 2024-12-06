using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Order> Get(int id)
        {
            return await DbContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public  async Task<PagedResult<Order>> List(int page, int pageSize)
        {
            return await DbContext.Orders
                .OrderBy(o => o.OrderDate)
                .GetPagedAsync(page, pageSize);
        }
    }
}
