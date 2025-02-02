using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderProductServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_orderProduct()
        {
            // Arrange
            var service = new OrderProductService(DbContext);

            // Loome dummy Order ja Product objektid.
            var order = new Order
            {
                // Lisa vajalikud omadused, näiteks:
                OrderDate = DateTime.UtcNow
            };
            var product = new Product
            {
                // Lisa vajalikud omadused, näiteks:
                Name = "Test Product"
            };

            // Salvesta need esmalt, et saada ID-d ja rahuldada FK seosed.
            DbContext.Orders.Add(order);
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = product.Id
                // Lisa vajadusel muid omadusi.
            };

            // Act
            await service.Save(orderProduct);

            // Assert
            var count = DbContext.OrderProducts.Count();
            var result = DbContext.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(order.Id, result.OrderId);
            Assert.Equal(product.Id, result.ProductId);
        }

        [Fact]
        public async Task Delete_should_remove_given_orderProduct()
        {
            // Arrange
            var service = new OrderProductService(DbContext);

            // Loome dummy Order ja Product objektid.
            var order = new Order { OrderDate = DateTime.UtcNow };
            var product = new Product { Name = "Test Product" };

            DbContext.Orders.Add(order);
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = product.Id
            };

            DbContext.OrderProducts.Add(orderProduct);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(orderProduct.Id);

            // Assert
            var count = DbContext.OrderProducts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_correct_orderProduct()
        {
            // Arrange
            var service = new OrderProductService(DbContext);

            // Loome dummy Order ja Product objektid.
            var order = new Order { OrderDate = DateTime.UtcNow };
            var product = new Product { Name = "Test Product" };

            DbContext.Orders.Add(order);
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = product.Id
            };

            DbContext.OrderProducts.Add(orderProduct);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(orderProduct.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderProduct.OrderId, result.OrderId);
            Assert.Equal(orderProduct.ProductId, result.ProductId);
            Assert.NotNull(result.Order);
            Assert.NotNull(result.Product);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var service = new OrderProductService(DbContext);

            // Loome dummy Order ja Product objektid.
            var order = new Order { OrderDate = DateTime.UtcNow };
            var product = new Product { Name = "Test Product" };

            DbContext.Orders.Add(order);
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            // Lisame mitu OrderProduct objekti, et testida leheküljestamist.
            for (int i = 0; i < 5; i++)
            {
                DbContext.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Id
                });
            }
            await DbContext.SaveChangesAsync();

            // Act: küsime esimese lehe, kus lehekülje suurus on 3.
            var pagedResult = await service.List(page: 1, pageSize: 3);

            // Assert
            Assert.NotNull(pagedResult);
            Assert.Equal(3, pagedResult.Items.Count);

            // Kontrollime, et andmed on järjestatud ID järgi kasvavas järjekorras.
            var expectedIds = DbContext.OrderProducts
                .OrderBy(op => op.Id)
                .Select(op => op.Id)
                .Take(3)
                .ToList();
            var actualIds = pagedResult.Items.Select(op => op.Id).ToList();
            Assert.Equal(expectedIds, actualIds);
        }
    }
}
