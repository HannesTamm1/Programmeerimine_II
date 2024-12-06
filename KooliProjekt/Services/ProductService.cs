using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Product>> List(int page, int pageSize)
        {
            return await _unitOfWork.ProductRepository.List(page, pageSize);
        }


        public async Task<Product> Get(int id)
        {
            return await _unitOfWork.ProductRepository.Get(id);
        }

        public async Task Save(Product product)
        {
            await _unitOfWork.ProductRepository.Save(product);
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.ProductRepository.Delete(id);
        }
    }
}
