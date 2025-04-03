using KooliProjekt.Models;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1, ProductsIndexModel model = null)
        {
            model = model ?? new ProductsIndexModel();
            model.Data = await _productService.List(page, 10, model.Search);
            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = await _productService.Get(id.Value);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.Save(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.Save(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

