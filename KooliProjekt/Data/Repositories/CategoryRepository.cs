using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Category> Get(int id)
        {
            return await DbContext.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public  async Task<PagedResult<Category>> List(int page, int pageSize)
        {
            return await DbContext.Categories
                .OrderBy(c => c.Name)
                .GetPagedAsync(page, pageSize);
        }
    }
}
