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
                .Include(list => list.ProductCatalog)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
