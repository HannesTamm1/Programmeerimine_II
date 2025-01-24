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
    public class OrderProductsControllerTests
    {
        private readonly Mock<IOrderProductService> _orderProductServiceMock;
        private readonly OrderProductsController _controller;

        public OrderProductsControllerTests()
        {
            _orderProductServiceMock = new Mock<IOrderProductService>();
            _controller = new OrderProductsController(_orderProductServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<OrderProduct>
            {
                new OrderProduct { Id = 1, Title = "Product 1" },
                new OrderProduct { Id = 2, Title = "Product 2" }
            };
            var pagedResult = new PagedResult<OrderProduct> { Results = data };
            _orderProductServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

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
        public async Task Details_Should_Return_NotFound_When_Product_Is_Missing()
        {
            // Arrange
            int id = 1;
            var product = (OrderProduct)null;
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_Should_Return_View_With_Model_When_Product_Was_Found()
        {
            // Arrange
            int id = 1;
            var product = new OrderProduct { Id = id, Title = "Product 1" };
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(product, result.Model);
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
        public async Task Edit_Should_Return_NotFound_When_Product_Is_Missing()
        {
            // Arrange
            int id = 1;
            var product = (OrderProduct)null;
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Should_Return_View_With_Model_When_Product_Was_Found()
        {
            // Arrange
            int id = 1;
            var product = new OrderProduct { Id = id, Title = "Product 1" };
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(product, result.Model);
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
        public async Task Delete_Should_Return_NotFound_When_Product_Is_Missing()
        {
            // Arrange
            int id = 1;
            var product = (OrderProduct)null;
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_View_With_Model_When_Product_Was_Found()
        {
            // Arrange
            int id = 1;
            var product = new OrderProduct { Id = id, Title = "Product 1" };
            _orderProductServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(product, result.Model);
        }
        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _orderProductServiceMock
                .Setup(x => x.Delete(id))
        .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _orderProductServiceMock.VerifyAll();
        }
    }
}

