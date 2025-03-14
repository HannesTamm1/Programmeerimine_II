using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Order>> List(int page, int pageSize, OrderSearch search = null)
        {
            var query = _context.Orders.AsQueryable();

            if (search != null)
            {
                if (!string.IsNullOrWhiteSpace(search.Title))
                {
                    query = query.Where(o => o.Title.Contains(search.Title));
                }
                if (!string.IsNullOrWhiteSpace(search.Status))
                {
                    query = query.Where(o => o.Status.Contains(search.Status));
                }
            }

            return await query
                .OrderBy(p => p.Title)
                .GetPagedAsync(page, pageSize);
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Order list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
