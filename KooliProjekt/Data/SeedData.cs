namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.Categories.Any() || context.Products.Any() || context.Orders.Any()) return;

            var categories = new List<Category>
        {
            new Category { Name = "Electronics" },
            new Category { Name = "Books" },
            new Category { Name = "Clothing" }
        };

            var products = new List<Product>
        {
            new Product { Name = "Laptop", Description = "High-end laptop", Price = 1200, Category = categories[0] },
            new Product { Name = "Headphones", Description = "Noise-cancelling", Price = 200, Category = categories[0] },
            new Product { Name = "Novel", Description = "Fiction book", Price = 15, Category = categories[1] }
        };

            context.Categories.AddRange(categories);
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
