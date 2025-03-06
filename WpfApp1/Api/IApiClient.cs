namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<List<Product>> List();
        Task Save(Product list);
        Task Delete(int id);
    }
}