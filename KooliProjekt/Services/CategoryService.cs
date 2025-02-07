using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);  // Find the category by ID
            if (category != null)  // Check if the category exists
            {
                _context.Categories.Remove(category);  // Remove the category
                await _context.SaveChangesAsync();     // Save changes to the database
            }
        }


        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<PagedResult<Category>> List(int page, int pageSize, CategoriesSearch search = null)
        {
            var query = _context.Categories.AsQueryable();

            search = search ?? new CategoriesSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(category => category.Name.Contains(search.Keyword));
            }

            // Return paginated results
            return await query
                .OrderBy(category => category.Name)
                .GetPagedAsync(page, pageSize);
        }

        public async Task Save(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                _context.Categories.Update(category);
            }
            await _context.SaveChangesAsync();
        }
    }
}
