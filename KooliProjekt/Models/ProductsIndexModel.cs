using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class ProductsIndexModel
    {
        public ProductSearch Search { get; set; }
        public PagedResult<Product> Data { get; set; }
    }
}
