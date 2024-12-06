namespace KooliProjekt.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<PagedResult<Product>> List(int page, int pageSize);
        Task Save(Product product);
        Task Delete(int id);
    }
}
