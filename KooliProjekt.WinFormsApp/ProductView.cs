using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    internal class ProductView : IProductView
    {
        public IList<Product> Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Product SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ProductPresenter Presenter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}