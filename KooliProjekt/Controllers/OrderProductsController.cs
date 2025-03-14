using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly IOrderProductService _orderProductService;

        public OrderProductsController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        public async Task<IActionResult> Index(OrderProductsSearch search, int page = 1, int pageSize = 10)
        {
            var model = new OrderProductsIndexModel
            {
                Search = search,
                Data = await _orderProductService.List(page, pageSize, search)
            };

            return View(model);
        }



        public async Task<IActionResult> Details(int id)
        {
            var orderProduct = await _orderProductService.Get(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            return View(orderProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                await _orderProductService.Save(orderProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(orderProduct);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var orderProduct = await _orderProductService.Get(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            return View(orderProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _orderProductService.Save(orderProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(orderProduct);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var orderProduct = await _orderProductService.Get(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            return View(orderProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderProductService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

