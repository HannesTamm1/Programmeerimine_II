using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

public class OrderProductService : IOrderProductService
{
    private readonly ApplicationDbContext _context;

    public OrderProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<OrderProduct>> List(int page, int pageSize, OrderProductsSearch search = null)
    {
        var query = _context.OrderProducts
            .Include(op => op.Order)
            .Include(op => op.Product)
            .AsQueryable();

        if (search != null)
        {
            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(op => op.Title.Contains(search.Keyword) ||
                                          op.ProductId.ToString().Contains(search.Keyword));
            }
            if (search.Done.HasValue && search.Done.Value)
            {
                query = query.Where(op => op.Order.Status == "Completed");
            }
        }

        return await query
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

