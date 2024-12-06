using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Product> Get(int id)
        {
            return await DbContext.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public  async Task<PagedResult<Product>> List(int page, int pageSize)
        {
            return await DbContext.Products
                .OrderBy(p => p.Name)
                .GetPagedAsync(page, pageSize);
        }
    }
}
