using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            // Check if there is any data in the database
            if (!context.Orders.Any() && !context.Products.Any() && !context.Users.Any() && !context.Categories.Any())
            {
                // Add sample Categories
                var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothing" },
                new Category { Name = "Books" },
                new Category { Name = "Toys" },
                new Category { Name = "Furniture" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();  // Save categories to database

                // Add sample Products
                var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "A powerful laptop", Category = categories[0], Price = 1000 },
                new Product { Name = "Smartphone", Description = "Latest model smartphone", Category = categories[0], Price = 800 },
                new Product { Name = "T-shirt", Description = "Comfortable cotton t-shirt", Category = categories[1], Price = 15 },
                new Product { Name = "Jeans", Description = "Stylish denim jeans", Category = categories[1], Price = 40 },
                new Product { Name = "Harry Potter", Description = "Fantasy book", Category = categories[2], Price = 20 },
                new Product { Name = "Puzzle", Description = "Fun puzzle game", Category = categories[3], Price = 10 },
                new Product { Name = "Sofa", Description = "Comfortable leather sofa", Category = categories[4], Price = 500 }
            };

                context.Products.AddRange(products);
                context.SaveChanges();  // Save products to database

                // Add sample Users
                var users = new List<User>
            {
                new User { Username = "john_doe", Email = "john@example.com" },
                new User { Username = "jane_doe", Email = "jane@example.com" }
            };

                context.Users.AddRange(users);
                context.SaveChanges();  // Save users to database

                // Add sample Orders
                var orders = new List<Order>
            {
new Order { OrderDate = DateTime.Now, Status = "Processing", UserId = users[0].Id.ToString(), User = users[0] },
new Order { OrderDate = DateTime.Now, Status = "Shipped", UserId = users[1].Id.ToString(), User = users[1] }
            };

                context.Orders.AddRange(orders);
                context.SaveChanges();  // Save orders to database

                // Optionally, you can add OrderProducts if necessary
                var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = orders[0].Id, ProductId = products[0].Id, PriceAtOrderTime = products[0].Price },
                new OrderProduct { OrderId = orders[1].Id, ProductId = products[2].Id, PriceAtOrderTime = products[2].Price }
            };

                context.OrderProducts.AddRange(orderProducts);
                context.SaveChanges();  // Save OrderProducts to database
            }
        }
    }
}
