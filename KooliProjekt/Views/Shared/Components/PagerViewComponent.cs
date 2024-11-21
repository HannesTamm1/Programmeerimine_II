using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<Product> products)
        {
            // Component logic here
            return Task.FromResult<IViewComponentResult>(View(products));
        }
    }
}
