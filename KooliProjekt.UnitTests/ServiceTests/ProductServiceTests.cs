using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_uowMock.Object);

            _uowMock.SetupGet(u => u.ProductRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Products()
        {
            // Arrange
            var results = new List<Product>
            {
                new Product { Id = 1 },
                new Product { Id = 2 }
            };
            var pagedResult = new PagedResult<Product> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _productService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Product()
        {
            // Arrange
            var product = new Product { Id = 1 };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.Get(1);

            // Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var product = new Product { Id = 1 };

            // Act
            await _productService.Save(product);

            // Assert
            _repositoryMock.Verify(r => r.Save(product), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _productService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}