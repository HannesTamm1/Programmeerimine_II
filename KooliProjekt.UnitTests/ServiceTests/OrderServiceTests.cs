using KooliProjekt.Data;
using KooliProjekt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new OrderService(DbContext);
            var order = new Order { Title = "Test" };

            // Act
            await service.Save(order);

            // Assert
            var count = DbContext.Orders.Count();
            var result = DbContext.Orders.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(order.Title, result.Title);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new OrderService(DbContext);
            var order = new Order { Title = "Test" };
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Orders.Count();
            Assert.Equal(0, count);
        }
    }
}
