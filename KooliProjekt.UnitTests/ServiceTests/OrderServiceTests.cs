using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_order()
        {
            // Arrange
            var service = new OrderService(DbContext);

            // Loome dummy User objekti, kuna Order sisaldab seotud Useri.
            var user = new User 
            { 
                // Lisa vajalikud omadused, näiteks:
                Username = "testuser", 
                Email = "test@example.com" 
            };
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            var order = new Order
            {
                // Näiteks määrame OrderDate, seome kasutajaga.
                OrderDate = DateTime.UtcNow,
                UserId = user.Id
                // OrderProducts saab olla null või tühi, kui seda ei vaja.
            };

            // Act
            await service.Save(order);

            // Assert
            var count = DbContext.Orders.Count();
            var result = DbContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(user.Id, result.UserId);
        }

        [Fact]
        public async Task Delete_should_remove_given_order()
        {
            // Arrange
            var service = new OrderService(DbContext);

            // Loome dummy User objekti.
            var user = new User 
            { 
                Username = "testuser", 
                Email = "test@example.com" 
            };
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                UserId = user.Id
            };

            DbContext.Orders.Add(order);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(order.Id);

            // Assert
            var count = DbContext.Orders.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_correct_order_with_includes()
        {
            // Arrange
            var service = new OrderService(DbContext);

            // Loome dummy andmed: User, Product ja OrderProduct
            var user = new User 
            { 
                Username = "testuser", 
                Email = "test@example.com" 
            };
            var product = new Product 
            { 
                Name = "Test Product" 
            };

            DbContext.Users.Add(user);
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                UserId = user.Id
            };
            DbContext.Orders.Add(order);
            await DbContext.SaveChangesAsync();

            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = product.Id
            };
            DbContext.OrderProducts.Add(orderProduct);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.NotNull(result.User);
            Assert.Equal(user.Id, result.User.Id);
            Assert.NotNull(result.OrderProducts);
            Assert.Contains(result.OrderProducts, op => op.ProductId == product.Id);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var service = new OrderService(DbContext);

            // Loome dummy User objekti.
            var user = new User 
            { 
                Username = "testuser", 
                Email = "test@example.com" 
            };
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            // Lisame mitu Order objekti, et testida leheküljestamist.
            for (int i = 0; i < 5; i++)
            {
                DbContext.Orders.Add(new Order
                {
                    OrderDate = DateTime.UtcNow.AddMinutes(i),
                    UserId = user.Id
                });
            }
            await DbContext.SaveChangesAsync();

            // Act: küsime esimese lehe, kus lehekülje suurus on 3.
            var pagedResult = await service.List(page: 1, pageSize: 3);

            // Assert
            Assert.NotNull(pagedResult);
            Assert.Equal(3, pagedResult.Items.Count);

            // Kontrollime, et tulemused on järjestatud (näiteks ID järgi, nagu teenuse meetodis)
            var expectedIds = DbContext.Orders
                .OrderBy(o => o.Id)
                .Select(o => o.Id)
                .Take(3)
                .ToList();
            var actualIds = pagedResult.Items.Select(o => o.Id).ToList();
            Assert.Equal(expectedIds, actualIds);
        }
    }
}
