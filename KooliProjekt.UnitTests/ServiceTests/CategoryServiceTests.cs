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
        public Task Save_should_add_new_list()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Title = "Test" };

            // Act
            service.Save(category);

            // Assert
            var count = DbContext.Categories.Count();
            var result = DbContext.Categories.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(category.Title, result.Title);
            return Task.CompletedTask;
        }

        private void Save(Category category)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Title = "Test" };
            DbContext.Categories.Add(category);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }
    }
}
