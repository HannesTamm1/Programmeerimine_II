using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class OrderProductService : IOrderProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<OrderProduct>> List(int page, int pageSize)
    {
        return await _unitOfWork.OrderProductRepository.List(page, pageSize);

    }

    public async Task<OrderProduct> Get(int id)
    {
        return await _unitOfWork.OrderProductRepository.Get(id);
    }

    public async Task Save(OrderProduct orderProduct)
    {
        await _unitOfWork.OrderProductRepository.Save(orderProduct);
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.OrderProductRepository.Delete(id);
    }
}

