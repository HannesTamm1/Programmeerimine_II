using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class CategoriesIndexModel
    {
        public CategoriesSearch Search { get; set; } 
        public PagedResult<Category> Data { get; set; } 
    }
}
