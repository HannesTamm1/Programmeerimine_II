namespace KooliProjekt.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<PagedResult<Product>> List(int page, int pageSize);
        Task Save(Product item);
        Task Delete(int id);
    }
}
