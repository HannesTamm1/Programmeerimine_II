using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ICategoryService
    {
        Task<PagedResult<Category>> List(int page, int pageSize);
        Task<Category> Get(int id);
        Task Save(Category category);
        Task Delete(int id);
    }
}
