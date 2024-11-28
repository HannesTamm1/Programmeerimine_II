using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

public class OrderProductService : IOrderProductService
{
    private readonly ApplicationDbContext _context;

    public OrderProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<OrderProduct>> List(int page, int pageSize)
    {
        return await _context.OrderProducts
            .Include(op => op.Order) // Include related data
            .Include(op => op.Product)
            .OrderBy(op => op.Id)
            .GetPagedAsync(page, pageSize);
    }

    public async Task<OrderProduct> Get(int id)
    {
        return await _context.OrderProducts
            .Include(op => op.Order)
            .Include(op => op.Product)
            .FirstOrDefaultAsync(op => op.Id == id);
    }

    public async Task Save(OrderProduct orderProduct)
    {
        if (orderProduct.Id == 0)
        {
            _context.OrderProducts.Add(orderProduct);
        }
        else
        {
            _context.OrderProducts.Update(orderProduct);
        }
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var orderProduct = await _context.OrderProducts.FindAsync(id);
        if (orderProduct != null)
        {
            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();
        }
    }
}

