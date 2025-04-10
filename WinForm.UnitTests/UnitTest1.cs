using KooliProjekt.WinFormsApp;
using KooliProjekt.WinFormsApp.Api;
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
            _presenter = new ProductPresenter(_productViewMock.Object, _apiClientMock.Object);
        }

        [Fact]
        public void UpdateView_ShouldClearView_WhenProductIsNull()
        {
            // Act
            _presenter.UpdateView(null);

            // Assert
            _productViewMock.VerifySet(v => v.Title = string.Empty);
            _productViewMock.VerifySet(v => v.Id = 0);
        }

        [Fact]
        public void UpdateView_ShouldSetViewProperties_WhenProductIsNotNull()
        {
            // Arrange
            var product = new Product { Id = 42, Title = "Test Product" };

            // Act
            _presenter.UpdateView(product);

            // Assert
            _productViewMock.VerifySet(v => v.Title = product.Title);
            _productViewMock.VerifySet(v => v.Id = product.Id);
        }

        [Fact]
        public async Task Load_ShouldPopulateProductView()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Title = "Product A" },
                new Product { Id = 2, Title = "Product B" }
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
        public async Task Delete_ShouldCallApiClientDelete()
        {
            // Arrange
            int productId = 99;

            // Act
            await _presenter.Delete(productId);

            // Assert
            _apiClientMock.Verify(api => api.Delete(productId), Times.Once);
        }

        [Fact]
        public async Task Save_ShouldCallApiClientSave()
        {
            // Arrange
            var product = new Product { Id = 3, Title = "To Be Saved" };

            // Act
            await _presenter.Save(product);

            // Assert
            _apiClientMock.Verify(api => api.Save(product), Times.Once);
        }
    }
}
