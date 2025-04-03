using Moq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1;
using WpfApp1.Api;
using Xunit;

namespace WpfApp1.Tests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _viewModel = new MainWindowViewModel(_apiClientMock.Object);
        }

        [Fact]
        public void NewCommand_ShouldCreateNewProduct()
        {
            // Arrange
            var initialCount = _viewModel.Lists.Count;

            // Act
            ((RelayCommand<Product>)_viewModel.NewCommand).Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedItem);
            Assert.IsType<Product>(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task SaveCommand_ShouldSaveProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            _viewModel.SelectedItem = product;

            // Act
            await Task.Run(() => ((RelayCommand<Product>)_viewModel.SaveCommand).Execute(null));

            // Assert
            _apiClientMock.Verify(api => api.Save(product), Times.Once);
            _apiClientMock.Verify(api => api.List(), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_ShouldDeleteProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            _viewModel.SelectedItem = product;
            _viewModel.Lists.Add(product);

            _viewModel.ConfirmDelete = p => true;

            // Act
            await Task.Run(() => ((RelayCommand<Product>)_viewModel.DeleteCommand).Execute(null));

            // Assert
            _apiClientMock.Verify(api => api.Delete(product.Id), Times.Once);
            Assert.DoesNotContain(product, _viewModel.Lists);
            Assert.Null(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task Load_ShouldPopulateLists()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };
            _apiClientMock.Setup(api => api.List()).ReturnsAsync(products);

            // Act
            await _viewModel.Load();

            // Assert
            Assert.Equal(products.Count, _viewModel.Lists.Count);
            foreach (var product in products)
            {
                Assert.Contains(product, _viewModel.Lists);
            }
        }
    }
}