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

namespace KooliProjekt.IntegrationTests.POST
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
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Categories");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/Categories/Details")]
        [InlineData("/Categories/Details/100")]
        [InlineData("/categories/Delete")]
        [InlineData("/Categories/Delete/100")]
        [InlineData("/categories/Edit")]
        [InlineData("/Categories/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Categories/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new Category { Name = "Hrry", Title = "Raamat" };
            _context.Categories.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync($"/Categories/Details/{list.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
    {
        { "Id", "0" },
        { "Name", "Test Category" },
        { "Title", "Test" }
    };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Categories/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.Categories.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Title);
            Assert.Equal("Test Category", list.Name);
        }


        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
    {
        { "Title", "" }
    };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Categories/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // Ensure the response status code is 400 (Bad Request)
            Assert.False(_context.Categories.Any()); // Ensure no categories were saved
        }



    }
}
