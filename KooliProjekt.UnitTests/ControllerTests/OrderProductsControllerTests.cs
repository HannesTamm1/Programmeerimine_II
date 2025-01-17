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
    }
}

