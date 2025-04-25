using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7136/api/")
            };
        }

        public async Task<Result<List<Product>>> List()
        {
            var result = new Result<List<Product>> { Value = new List<Product>() };

            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
                result.Value = products ?? new List<Product>();
            }
            catch (HttpRequestException)
            {
                result.Error = "Ei saa serveriga ühendust. Palun proovi hiljem uuesti.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result<Product>> Get(int id)
        {
            var result = new Result<Product>();

            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>($"Products/{id}");
                result.Value = product;
            }
            catch (HttpRequestException)
            {
                result.Error = "Ei saa serveriga ühendust. Palun proovi hiljem uuesti.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
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
                await _httpClient.PutAsJsonAsync($"Products/{product.Id}", product);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"Products/{id}");
        }
    }
}
