using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests.GET
{
    [Collection("Sequential")]
    public class OrdersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public OrdersControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_success()
        {
            // Act
            using var response = await _client.GetAsync("/Orders");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_order_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/Orders/Details/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_order_exists()
        {
            // Arrange
            var user = new User { Username = "TestUser", Email = "test@example.com" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = user.Id,
                Status = "Pending"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/Orders/Details/{order.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_order_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/Orders/Delete/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
