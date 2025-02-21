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
            using var response = await _client.GetAsync("/OrderProducts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/OrderProducts/Details")]
        [InlineData("/OrderProducts/Details/100")]
        [InlineData("/OrderProducts/Delete")]
        [InlineData("/OrderProducts/Delete/100")]
        [InlineData("/OrderProducts/Edit")]
        [InlineData("/OrderProducts/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            // Act
            using var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Act
            using var response = await _client.GetAsync("/OrderProducts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new OrderProduct { Title = "Test" };
            _context.OrderProducts.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/OrderProducts/Details/{list.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_order_product()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Id", "0" },
                { "Title", "Test" }
            };
            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/OrderProducts/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.OrderProducts.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Title);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_order_product()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Title", "" }
            };
            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/OrderProducts/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.OrderProducts.Any());
        }
    }
}
