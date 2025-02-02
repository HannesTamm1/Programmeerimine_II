using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CategoryServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_category()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Name = "Test Category" };

            // Act
            await service.Save(category);

            // Assert
            var count = DbContext.Categories.Count();
            var result = DbContext.Categories.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async Task Delete_should_remove_given_category()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Name = "Test Category" };
            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(category.Id);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_correct_category()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            var category = new Category { Name = "Test Category" };
            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async Task List_should_return_paged_result_with_search_keyword()
        {
            // Arrange
            var service = new CategoryService(DbContext);
            // Lisame mitu kategooriat, et testida lehitsemist ja leheküljestamist
            DbContext.Categories.AddRange(
                new Category { Name = "Alpha" },
                new Category { Name = "Beta" },
                new Category { Name = "Gamma" }
            );
            await DbContext.SaveChangesAsync();

            // Act
            // Otsime kategooriaid, mille nimes sisaldub tähemärk 'a' (case-insensitive otsing sõltub andmebaasist ja konfiguratsioonist)
            var pagedResult = await service.List(page: 1, pageSize: 2, new CategoriesSearch { Keyword = "a" });

            // Assert
            Assert.NotNull(pagedResult);
            Assert.True(pagedResult.Items.Count => 2);
            // Kontrollime, et kõik leitud kategooriad sisaldavad otsitavat märki
            Assert.All(pagedResult.Items, item =>
                Assert.Contains("a", item.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}

