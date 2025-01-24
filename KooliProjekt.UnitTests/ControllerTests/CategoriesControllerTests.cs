using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _controller = new CategoriesController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var model = new CategoriesIndexModel();
            var data = new PagedResult<Category>
            {
                Results = new List<Category>
                {
                    new Category { Id = 1, Name = "Category 1" },
                    new Category { Id = 2, Name = "Category 2" }
                }
            };

            _categoryServiceMock.Setup(x => x.List(page, 10, null)).ReturnsAsync(data);

            // Act
            var result = await _controller.Index(page, model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data, model.Data);
            Assert.Equal(model, result.Model);
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
        public async Task Details_should_return_notfound_when_category_is_missing()
        {
            // Arrange
            int id = 1;
            var category = (Category)null;
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_category_was_found()
        {
            // Arrange
            int id = 1;
            var category = new Category { Id = id, Name = "Category 1" };
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(category, result.Model);
        }

        [Fact]
        public void Create_should_return_view()
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
        public async Task Edit_should_return_notfound_when_category_is_missing()
        {
            // Arrange
            int id = 1;
            var category = (Category)null;
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_category_was_found()
        {
            // Arrange
            int id = 1;
            var category = new Category { Id = id, Name = "Category 1" };
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(category, result.Model);
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
        public async Task Delete_should_return_notfound_when_category_is_missing()
        {
            // Arrange
            int id = 1;
            var category = (Category)null;
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_category_was_found()
        {
            // Arrange
            int id = 1;
            var category = new Category { Id = id, Name = "Category 1" };
            _categoryServiceMock.Setup(x => x.Get(id)).ReturnsAsync(category);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(category, result.Model);
        }
        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _categoryServiceMock
                .Setup(x => x.Delete(id))
        .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _categoryServiceMock.VerifyAll();
        }
    }
}
