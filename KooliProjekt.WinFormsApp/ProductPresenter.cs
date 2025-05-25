using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public class ProductPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IProductView _productView;

        public ProductPresenter(IProductView productView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _productView = productView;

            productView.Presenter = this;
        }

        public void UpdateView(Product product)
        {
            if (product == null)
            {
                _productView.Name = string.Empty;
                _productView.Id = 0;
            }
            else
            {
                _productView.Id = product.Id;
                _productView.Name = product.Name;
            }
        }

        public async Task Load()
        {
            var productsResult = await _apiClient.List();
            _productView.Products = productsResult.Value; 
        }
    }
}