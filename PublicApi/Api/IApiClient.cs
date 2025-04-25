using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI.Api
{
    public interface IApiClient
    {
        Task<Result<List<Product>>> List();
        Task Save(Product list);
        Task Delete(int id);
    }
}