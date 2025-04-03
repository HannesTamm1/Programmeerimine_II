using System.Net.Http;
using System.Net.Http.Json;
using WpfApp1.Api;

namespace KooliProjekt.WpfApp.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<List<Product>> List()
        {
            var result = new List<Product>();

            try
            {
                result = await _httpClient.GetFromJsonAsync<List<Product>>("Product");
            }
            catch (Exception ex)
            {
                // Handle the error appropriately
                throw new Exception("Error fetching product list", ex);
            }

            return result;
        }

        public async Task Save(Product product)
        {
            if (product.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Products", product);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Products/" + product.Id, product);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Products/" + id);
        }
    }
}