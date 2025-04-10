using KooliProjekt.WinFormsApp.Api;
using System;
using System.Windows.Forms;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IProductView
    {
        private ProductPresenter _presenter;

        public Form1(ProductPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.Load().Wait(); // Modified this line
        }

        public IList<Product> Products
        {
            get => (IList<Product>)TodoListsGrid.DataSource;
            set => TodoListsGrid.DataSource = value;
        }

        public Product SelectedItem
        {
            get => (Product)TodoListsGrid.CurrentRow?.DataBoundItem;
            set
            {
                if (value != null)
                {
                    IdField.Text = value.Id.ToString();
                    TitleField.Text = value.Title;
                }
            }
        }

        public string Title
        {
            get => TitleField.Text;
            set => TitleField.Text = value;
        }

        public int Id
        {
            get => int.Parse(IdField.Text);
            set => IdField.Text = value.ToString();
        }

        public ProductPresenter Presenter
        {
            get => _presenter;
            set => _presenter = value;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            _presenter.UpdateView(new Product { Title = "New Product" });
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            var product = new Product { Id = Id, Title = Title };
            await _presenter.Save(product);
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var product = SelectedItem;
            if (product != null)
            {
                await _presenter.Delete(product.Id);
            }
        }
    }
    public class ProductPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IProductView _view;

        public ProductPresenter(IApiClient apiClient, IProductView view)
        {
            _apiClient = apiClient;
            _view = view;
        }

        public void UpdateView(Product product)
        {
            // Implementation
        }

        public async Task Load()
        {
            var products = await _apiClient.List();
            _view.Products = products;
        }

        public async Task Delete(int productId)
        {
            await _apiClient.Delete(productId);
        }

        internal async Task Save(Product product)
        {
            await _apiClient.Save(product);
        }
    }
}
