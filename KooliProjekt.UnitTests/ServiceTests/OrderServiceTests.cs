using KooliProjekt.Data;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IOrderRepository>();
            _orderService = new OrderService(_uowMock.Object);

            _uowMock.SetupGet(u => u.OrderRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Orders()
        {
            // Arrange
            var results = new List<Order>
            {
                new Order { Id = 1, UserId = "1", User = new User { Id = 1, Username = "testuser1", Email = "user1@example.com" }, Status = "Pending" },
                new Order { Id = 2, UserId = "2", User = new User { Id = 2, Username = "testuser2", Email = "user2@example.com" }, Status = "Completed" }
            };
            var pagedResult = new PagedResult<Order> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _orderService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Order()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = "1", User = new User { Id = 1, Username = "testuser1", Email = "user1@example.com" }, Status = "Pending" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(order);

            // Act
            var result = await _orderService.Get(1);

            // Assert
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = "1", User = new User { Id = 1, Username = "testuser1", Email = "user1@example.com" }, Status = "Pending" };

            // Act
            await _orderService.Save(order);

            // Assert
            _repositoryMock.Verify(r => r.Save(order), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _orderService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
