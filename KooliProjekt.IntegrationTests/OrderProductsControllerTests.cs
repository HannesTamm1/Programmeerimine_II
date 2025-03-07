using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class OrderProductsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public OrderProductsControllerTests()
        {
            _client = Factory.CreateClient();
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/OrderProducts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/OrderPoducts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_order_product_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/OrderProducts/Details/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_order_product_exists()
        {
            // Arrange
            var Category1 = new Category { Name = "Tooted", Title = "Tooted2" };
            _context.Categories.Add(Category1);
            _context.SaveChanges();

            var Product1 = new Product { Name = "first item", CategoryId = Category1.Id };

            var User1 = new User
            {
                Email = "example@gmail.com",
                Username = "Test",
            };

            var Order1 = new Order { Title = "Esimene", Status = "Pending", User = User1, UserId = User1.Id };

            var OrderProduct = new OrderProduct { Product = Product1, ProductId = Product1.Id, Order = Order1, PriceAtOrderTime = 10 };
            _context.OrderProducts.Add(OrderProduct);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/OrderProducts/Details/{OrderProduct.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}