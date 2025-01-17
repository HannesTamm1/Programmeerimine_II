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
    }
}
