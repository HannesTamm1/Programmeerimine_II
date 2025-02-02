using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProductServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_product()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product
            {
                Name = "Test Product",
                Price = 100.0m
            };

            // Act
            await service.Save(product);

            // Assert
            var count = await DbContext.Products.CountAsync();
            var result = await DbContext.Products.FirstOrDefaultAsync();
            Assert.Equal(1, count);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task Delete_should_remove_given_product()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product
            {
                Name = "Test Product",
                Price = 100.0m
            };

            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(product.Id);

            // Assert
            var count = await DbContext.Products.CountAsync();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_correct_product()
        {
            // Arrange
            var service = new ProductService(DbContext);
            var product = new Product
            {
                Name = "Test Product",
                Price = 100.0m
            };

            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task List_should_return_paged_result_with_search_filters()
        {
            // Arrange
            var service = new ProductService(DbContext);

            // Lisame mitu toodet, et testida otsingut ja leheküljestamist.
            var products = new[]
            {
                new Product { Name = "Apple", Price = 50.0m },
                new Product { Name = "Banana", Price = 30.0m },
                new Product { Name = "Cherry", Price = 70.0m },
                new Product { Name = "Date", Price = 90.0m },
                new Product { Name = "Elderberry", Price = 110.0m }
            };
            DbContext.Products.AddRange(products);
            await DbContext.SaveChangesAsync();

            // Act
            // Otsime tooteid, mille nimes sisaldub "a" (kasutame ToLower võrdlemiseks) 
            // ja mille hind on vahemikus 40 kuni 100.
            var search = new ProductSearch
            {
                Keyword = "a",
                MinPrice = 40.0m,
                MaxPrice = 100.0m
            };

            var pagedResult = await service.List(page: 1, pageSize: 3, search: search);

            // Assert
            Assert.NotNull(pagedResult);

            // Filtreerime käsitsi oodatud tulemused.
            var expectedProducts = DbContext.Products
                .Where(p => p.Name.ToLower().Contains("a") &&
                            p.Price >= 40.0m &&
                            p.Price <= 100.0m)
                .OrderBy(p => p.Name)
                .ToList();

            Assert.Equal(expectedProducts.Count, pagedResult.Items.Count);

            // Kontrollime, et kõik tagastatud tooted vastavad otsingu tingimustele.
            foreach (var prod in pagedResult.Items)
            {
                Assert.Contains("a", prod.Name.ToLower());
                Assert.InRange(prod.Price, 40.0m, 100.0m);
            }
        }
    }
}
