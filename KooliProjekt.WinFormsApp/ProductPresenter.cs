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

        public void UpdateView(Product list)
        {
            if (list == null)
            {
                _productView.Name = string.Empty;
                _productView.Id = 0;
            }
            else
            {
                _productView.Id = list.Id;
                _productView.Name = list.Name;
            }
        }

        public async Task Load()
        {
            var product = await _apiClient.List();

            _productView.Products = Product.Value;
        }
    }
}