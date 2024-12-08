using KooliProjekt.Search;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> List(int page, int pageSize, ProductSearch search = null)
        {
            var query = _context.Products.AsQueryable();

            search = search ?? new ProductSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(p => p.Name.Contains(search.Keyword));
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= search.MinPrice.Value);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= search.MaxPrice.Value);
            }

            return await query
                .OrderBy(p => p.Name)
                .GetPagedAsync(page, pageSize);
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Save(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}

