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
    public class ProductsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public ProductsControllerTests()
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
            using var response = await _client.GetAsync("/Products");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_product_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/Products/Details/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_product_exists()
        {
            // Arrange
            var category = new Category { Name = "Test Category" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var product = new Product { Name = "Test Product", CategoryId = category.Id, Price = 10 };
            _context.Products.Add(product);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/Products/Details/{product.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_product_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/Products/Delete/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}