using System.Net.Http;
using System.Net.Http.Json;

namespace WpfApp1.Api
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
            var result = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
            return result;
        }

        public async Task Save(Product list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Products", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Products/" + list.Id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Products/" + id);
        }
    }
}