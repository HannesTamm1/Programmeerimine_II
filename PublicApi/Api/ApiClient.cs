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
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<Product>>> List()
        {
            var result = new Result<List<Product>>();

            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
                result.Value = products ?? new List<Product>(); // Ensure non-null assignment
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result<Product>> Save(Product product)
        {
            HttpResponseMessage response;
            Result<Product> result = new Result<Product>();

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
                var productResult = await response.Content.ReadFromJsonAsync<Product>();
                result.Value = productResult ?? new Product(); // Ensure non-null assignment
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                if (errorResult != null)
                {
                    // Copy errors from errorResult to result
                    foreach (var kvp in errorResult.Errors)
                    {
                        foreach (var msg in kvp.Value)
                        {
                            result.AddError(kvp.Key, msg);
                        }
                    }
                }
                else
                {
                    result.AddError("_", "Unknown error");
                }
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync("Products/" + id);
            var result = new Result();

            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                if (errorResult != null)
                {
                    foreach (var kvp in errorResult.Errors)
                    {
                        foreach (var msg in kvp.Value)
                        {
                            result.AddError(kvp.Key, msg);
                        }
                    }
                }
                else
                {
                    result.AddError("_", "Unknown error");
                }
            }

            return result;
        }

        public async Task<Result<Product>> Get(int id)
        {
            var result = new Result<Product>();

            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>("Products/" + id);
                result.Value = product ?? new Product(); // Ensure non-null assignment
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }
    }
}