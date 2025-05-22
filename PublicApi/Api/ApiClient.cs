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
                result.Value = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result> Save(Product list)
        {
            HttpResponseMessage response;

            if (list.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Products", list);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync("Products/" + list.Id, list);
            }

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result>();
                return result;
            }

            return new Result();
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Products/" + id);
        }

        public async Task<Result<Product>> Get(int id)
        {
            var result = new Result<Product>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<Product>("Products/" + id);
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        Task<Result<List<Product>>> IApiClient.List()
        {
            throw new NotImplementedException();
        }

        Task<Result<Product>> IApiClient.Save(Product product)
        {
            throw new NotImplementedException();
        }

        Task<Result> IApiClient.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<Product>> IApiClient.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
