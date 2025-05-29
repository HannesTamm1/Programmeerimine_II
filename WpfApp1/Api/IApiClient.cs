using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<List<Product>>> List();
        Task<Result<Product>> Save(Product product);
        Task<Result> Delete(int id);
    }
}