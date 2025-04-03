using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CategoryServiceTests : ServiceTestBase
    {

        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category
            {
                Title = "Test",
                Name = "Test Category"  // ✅ Add required Name field
            };

            await service.Save(category);

            // Assert
            var count = DbContext.Categories.Count();
            var result = DbContext.Categories.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(category.Title, result.Title);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Id = 1, Title = "Test", Name = "Test Category" };
            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            var categoryId = category.Id;

            // Act
            await service.Delete(categoryId);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }
    }
}
