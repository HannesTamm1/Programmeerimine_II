using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KooliProjekt.IntegrationTests.POST
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
            var user = new User { Username = "TestUser", Email = "testuser@example.com" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var category = new Category { Name = "TestCategory", Title = "TestCategoryTitle" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var product = new Product { Name = "TestProduct", Category = category };
            _context.Products.Add(product);
            _context.SaveChanges();

            var order = new Order
            {
                Title = "TestOrder",
                Status = "New",
                UserId = user.Id,
                User = user,
                OrderDate = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            var orderProduct = new OrderProduct { Title = "Test", Product = product, Order = order };
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Orders/Details/" + orderProduct.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }




        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var user = new User { Username = "TestUser", Email = "testuser@example.com" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var formValues = new Dictionary<string, string>
    {
        { "Id", "0" },
        { "Title", "Test" },
        { "Status", "New" },
        { "UserId", user.Id.ToString() }
    };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Orders/Create", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.Orders.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Title);
            Assert.Equal("New", list.Status);
            Assert.Equal(user.Id, list.UserId);
        }



        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var category = new Category { Name = "TestCategory", Title = "TestCategoryTitle" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var formValues = new Dictionary<string, string>
                {
                    { "Name", "" },
                    { "Description", "" },
                    { "Price", "0" },
                    { "CategoryId", category.Id.ToString() }
                };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Products/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Products.Any()); 
        }
    }
}
