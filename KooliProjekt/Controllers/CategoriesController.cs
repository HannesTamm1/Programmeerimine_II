using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1, CategoriesIndexModel model = null)
        {
            model = model ?? new CategoriesIndexModel();
            model.Data = await _categoryService.List(page, 10, model.Search);
            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.Save(category);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryService.Save(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

