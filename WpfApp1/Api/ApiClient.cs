﻿using KooliProjekt.WpfApp.Api;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        public async Task<Result> Save(Product product)
        {
            var result = new Result();

            try
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
                await _httpClient.DeleteAsync("Products/" + id);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
