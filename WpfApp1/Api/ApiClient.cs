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

        public async Task<Result<List<Product>>> List()
        {
            var result = new Result<List<Product>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result<Product>> Save(Product product)
        {
            var result = new Result<Product>();

            try
            {
                HttpResponseMessage response;

                if (product.Id == 0)
                {
                    response = await _httpClient.PostAsJsonAsync("Products", product);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync("Products/" + product.Id, product);
                }

                if (response.IsSuccessStatusCode)
                {
                    result.Value = await response.Content.ReadFromJsonAsync<Product>();
                }
                else
                {
                    result.Error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                var response = await _httpClient.DeleteAsync("Products/" + id);

                if (!response.IsSuccessStatusCode)
                {
                    result.Error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
    }
}