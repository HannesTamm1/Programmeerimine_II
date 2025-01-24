using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new OrdersController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<Order>
            {
                new Order { Id = 1, Title = "Product 1", Status = "" },
                new Order { Id = 2, Title = "Product 2", Status = "" }
            };
            var pagedResult = new PagedResult<Order> { Results = data };
            _orderServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);
            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
        [Fact]
        public async Task Details_Should_Return_NotFound_When_Id_Is_Missing()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_Should_Return_NotFound_When_Order_Is_Missing()
        {
            // Arrange
            int id = 1;
            var order = (Order)null;
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_Should_Return_View_With_Model_When_Order_Was_Found()
        {
            // Arrange
            int id = 1;
            var order = new Order { Id = id };
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(order, result.Model);
        }
        [Fact]
        public void Create_Should_Return_View()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Create"
            );
        }
        [Fact]
        public async Task Edit_Should_Return_NotFound_When_Id_Is_Missing()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_Should_Return_NotFound_When_Order_Is_Missing()
        {
            // Arrange
            int id = 1;
            var order = (Order)null;
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_Should_Return_View_With_Model_When_Order_Was_Found()
        {
            // Arrange
            int id = 1;
            var order = new Order { Id = id };
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(order, result.Model);
        }
        [Fact]
        public async Task Delete_Should_Return_NotFound_When_Id_Is_Missing()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_Should_Return_NotFound_When_Order_Is_Missing()
        {
            // Arrange
            int id = 1;
            var order = (Order)null;
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_Should_Return_View_With_Model_When_Order_Was_Found()
        {
            // Arrange
            int id = 1;
            var order = new Order { Id = id };
            _orderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(order);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(order, result.Model);
        }
        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _orderServiceMock
                .Setup(x => x.Delete(id))
        .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _orderServiceMock.VerifyAll();
        }
    }
}
