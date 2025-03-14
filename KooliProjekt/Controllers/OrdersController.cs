using System.Windows.Controls;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: TodoLists
        public async Task<IActionResult> Index(OrderSearch search, int page = 1, int pageSize = 10)
        {
            var model = new OrdersIndexModel
            {
                Search = search,
                Data = await _orderService.List(page, pageSize, search)
            };

            return View(model);
        }

        // GET: TodoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.Get(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: TodoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _orderService.Get(id.Value);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // GET: TodoLists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Unit,UnitCost,Manufacturer")] Order orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _orderService.Save(orders);
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

    

        // GET: TodoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.Get(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: TodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}