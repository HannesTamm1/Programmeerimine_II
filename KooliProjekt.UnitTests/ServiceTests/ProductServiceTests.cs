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
    public class ProductServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product { Name = "Test" };

            // Act
            await service.Save(product);

            // Assert
            var count = DbContext.Products.Count();
            var result = DbContext.Products.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product { Name = "Test" };
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
