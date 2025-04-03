using KooliProjekt.WpfApp.Api;

namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<List<Product>>> List();
        Task<Result> Save(Product product);
        Task<Result> Delete(int id);
    }
}
