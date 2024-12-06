using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OrderProduct> Get(int id)
        {
            return await DbContext.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Where(op => op.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<OrderProduct>> List(int page, int pageSize)
        {
            return await DbContext.OrderProducts
                .OrderBy(op => op.Id)
                .GetPagedAsync(page, pageSize);
        }
    }
}
