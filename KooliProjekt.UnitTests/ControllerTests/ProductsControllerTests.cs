using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Product>
            {
                new Product { Id = 1, Name = "Nimi1"},
                new Product { Id = 2, Name = "Nimi2"}
            };
            var pagedResult = new PagedResult<Product> { Results = data };
            _productServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = result.Model as ProductsIndexModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);


        }
        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_should_return_notfound_when_product_is_missing()
        {
            // Arrange
            int id = 1;
            Product product = null;
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task Details_should_return_view_with_model_when_product_was_found()
        {
            // Arrange
            int id = 1;
            var product = new Product { Id = id, Name = "Product1" };
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product, result?.Model);
        }
        [Fact]
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_should_return_notfound_when_product_is_missing()
        {
            // Arrange
            int id = 1;
            Product product = null;
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_should_return_view_with_model_when_product_was_found()
        {
            // Arrange
            int id = 1;
            var product = new Product { Id = id, Name = "Product1" };
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product, result?.Model);
        }
        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_should_return_notfound_when_product_is_missing()
        {
            // Arrange
            int id = 1;
            Product product = null;
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_should_return_view_with_model_when_product_was_found()
        {
            // Arrange
            int id = 1;
            var product = new Product { Id = id, Name = "Product1" };
            _productServiceMock.Setup(x => x.Get(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product, result?.Model);
        }
        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _productServiceMock
                .Setup(x => x.Delete(id))
        .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _productServiceMock.VerifyAll();
        }
    }
}
