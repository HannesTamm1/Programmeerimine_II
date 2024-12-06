namespace KooliProjekt.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Get(int id);
        Task<PagedResult<Category>> List(int page, int pageSize);
        Task Save(Category category);
        Task Delete(int id);
    }
}