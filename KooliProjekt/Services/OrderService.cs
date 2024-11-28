using KooliProjekt.Data;
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

        public async Task<PagedResult<Order>> List(int page, int pageSize)
        {
            return await _context.Orders
                .Include(o => o.User) // Include related User
                .Include(o => o.OrderProducts) // Include related OrderProducts
                .GetPagedAsync(page, pageSize);
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product) // Include products for the order
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task Save(Order order)
        {
            if (order.Id == 0)
            {
                _context.Add(order);
            }
            else
            {
                _context.Update(order);
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
