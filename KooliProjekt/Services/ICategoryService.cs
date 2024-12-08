using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ICategoryService
    {
        Task<PagedResult<Category>> List(int page, int pageSize, CategoriesSearch search = null);
        Task<Category> Get(int id);
        Task Save(Category list);
        Task Delete(int id);
    }
}