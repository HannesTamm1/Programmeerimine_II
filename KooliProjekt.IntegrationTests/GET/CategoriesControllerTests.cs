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
    public class CategoriesControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public CategoriesControllerTests()
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
            using var response = await _client.GetAsync("/Categories");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_category_does_not_exist()
        {
            // Act
            using var response = await _client.GetAsync("/Categories/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Act
            using var response = await _client.GetAsync("/Categories/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_category_exists()
        {
            // Arrange
            var category = new Category { Name = "Test Category", Title = "Test Title" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/Categories/Details/{category.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}