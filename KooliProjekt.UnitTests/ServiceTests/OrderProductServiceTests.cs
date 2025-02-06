using KooliProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderProductServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new OrderProductService(DbContext);
            var orderproduct = new OrderProduct { Title = "Test" };

            // Act
            await service.Save(orderproduct);

            // Assert
            var count = DbContext.OrderProducts.Count();
            var result = DbContext.OrderProducts.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(orderproduct.Title, result.Title);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new OrderProductService(DbContext);
            var orderproduct = new OrderProduct { Title = "Test" };
            DbContext.OrderProducts.Add(orderproduct);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.OrderProducts.Count();
            Assert.Equal(0, count);
        }
    }
}
