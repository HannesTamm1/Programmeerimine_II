using KooliProjekt.PublicAPI.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.WinFormsApp
{
    public class ProductPresenter
    {
        public readonly IApiClient _apiClient;
        public readonly IProductView _productView;

        public ProductPresenter(IProductView view, IApiClient apiClient)
        {
            _productView = view ?? throw new ArgumentNullException(nameof(view));
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public void UpdateView(Product product)
        {
            if (product == null)
            {
                _productView.Title = string.Empty;
                _productView.Id = 0;
            }
            else
            {
                _productView.Title = product.Name;
                _productView.Id = product.Id;
            }
        }

        public async Task Load()
        {
            var response = await _apiClient.List();
            _productView.Products = response.Value;
        }

        public async Task Delete(int productId)
        {
            await _apiClient.Delete(productId);
        }

        public async Task Save(Product product)
        {
            await _apiClient.Save(product);
        }
    }
}
