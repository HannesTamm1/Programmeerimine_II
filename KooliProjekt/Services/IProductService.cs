using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IProductService
    {
        Task<PagedResult<Product>> List(int page, int pageSize, ProductSearch search = null);
        Task<Product> Get(int id);
        Task Save(Product product);
        Task Delete(int id);
    }
}
