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
    public class ProductServiceTests
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product { Category = "Test" };

            // Act
            await service.Save(product);

            // Assert
            var count = DbContext.TodoLists.Count();
            var result = DbContext.TodoLists.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(product.Category, result.Title);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product { Category = "Test" };
            DbContext.Products.Add(product);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Products.Count();
            Assert.Equal(0, count);
        }
    }
}
