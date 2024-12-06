using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Order>> List(int page, int pageSize)
        {
            return await _unitOfWork.OrderRepository.List(page, pageSize);
        }

        public async Task<Order> Get(int id)
        {
            return await _unitOfWork.OrderRepository.Get(id);
        }

        public async Task Save(Order order)
        {
            await _unitOfWork.OrderRepository.Save(order);
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.OrderRepository.Delete(id);
        }
    }
}