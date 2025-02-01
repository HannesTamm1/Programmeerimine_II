using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IOrderProductRepository> _repositoryMock;
        private readonly OrderProductService _orderProductService;

        public OrderProductServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IOrderProductRepository>();
            _orderProductService = new OrderProductService(_uowMock.Object);

            _uowMock.SetupGet(u => u.OrderProductRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_OrderProducts()
        {
            // Arrange
            var results = new List<OrderProduct>
            {
                new OrderProduct { Id = 1 },
                new OrderProduct { Id = 2 }
            };
            var pagedResult = new PagedResult<OrderProduct> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _orderProductService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_OrderProduct()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1 };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(orderProduct);

            // Act
            var result = await _orderProductService.Get(1);

            // Assert
            Assert.Equal(orderProduct, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1 };

            // Act
            await _orderProductService.Save(orderProduct);

            // Assert
            _repositoryMock.Verify(r => r.Save(orderProduct), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _orderProductService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
