using KooliProjekt.WinFormsApp;
using KooliProjekt.PublicAPI.Api;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.WinFormsApp.Tests
{
    public class ProductPresenterTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IProductView> _productViewMock;
        private readonly ProductPresenter _presenter;

        public ProductPresenterTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _productViewMock = new Mock<IProductView>();

            // Setup Products property for Load
            _productViewMock.SetupProperty(v => v.Products);

            _presenter = new ProductPresenter(_productViewMock.Object, _apiClientMock.Object);
        }

        [Fact]
        public void UpdateView_ShouldClearView_WhenProductIsNull()
        {
            // Act
            _presenter.UpdateView(null!);

            // Assert
            _productViewMock.VerifySet(v => v.Name = string.Empty, Times.Once);
            _productViewMock.VerifySet(v => v.Id = 0, Times.Once);
        }

        [Fact]
        public void UpdateView_ShouldSetViewProperties_WhenProductIsNotNull()
        {
            // Arrange
            var product = new Product { Id = 42, Name = "Test Product" };

            // Act
            _presenter.UpdateView(product);

            // Assert
            _productViewMock.VerifySet(v => v.Name = product.Name, Times.Once);
            _productViewMock.VerifySet(v => v.Id = product.Id, Times.Once);
        }

        [Fact]
        public async Task Load_ShouldPopulateProductView()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A" },
                new Product { Id = 2, Name = "Product B" }
            };

            _apiClientMock
                .Setup(api => api.List())
                .ReturnsAsync(new Result<List<Product>> { Value = products });

            // Act
            await _presenter.Load();

            // Assert
            _productViewMock.VerifySet(v => v.Products = products, Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldCallApiClientDelete_AndLoad()
        {
            // Arrange
            int productId = 99;
            _apiClientMock.Setup(api => api.Delete(productId)).ReturnsAsync(new Result());
            _apiClientMock.Setup(api => api.List()).ReturnsAsync(new Result<List<Product>> { Value = new List<Product>() });

            // Act
            await _presenter.Delete(productId);

            // Assert
            _apiClientMock.Verify(api => api.Delete(productId), Times.Once);
            _apiClientMock.Verify(api => api.List(), Times.Once);
            _productViewMock.VerifySet(v => v.Products = It.IsAny<IList<Product>>(), Times.Once);
        }

        [Fact]
        public async Task Save_ShouldCallApiClientSave_AndLoad()
        {
            // Arrange
            var product = new Product { Id = 3, Name = "To Be Saved" };
            _apiClientMock.Setup(api => api.Save(product)).ReturnsAsync(new Result<Product>());
            _apiClientMock.Setup(api => api.List()).ReturnsAsync(new Result<List<Product>> { Value = new List<Product>() });

            // Act
            await _presenter.Save(product);

            // Assert
            _apiClientMock.Verify(api => api.Save(product), Times.Once);
            _apiClientMock.Verify(api => api.List(), Times.Once);
            _productViewMock.VerifySet(v => v.Products = It.IsAny<IList<Product>>(), Times.Once);
        }
    }
}