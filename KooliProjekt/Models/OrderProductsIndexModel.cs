using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class OrderProductsIndexModel
    {
        public OrderProductsSearch Search { get; set; }
        public PagedResult<OrderProduct> Data { get; set; }
    }
}
