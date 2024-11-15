namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void GenerateProducts(ApplicationDbContext context)
        {
            // Kontrollime, kas andmebaasis on juba andmeid tabelis "Products"
            if (context.Products.Any())
            {
                Console.WriteLine(context.Products.Any());
                return;
            }

            var product1 = new Product();
            product1.Name = "Samsung Galaxy S21";
            product1.Description = "128GB, Phantom Gray";
            product1.Photo = "galaxy_s21.jpg";
            product1.Price = 799;
            product1.Discount = 10;

            var product2 = new Product();
            product2.Name = "Apple iPhone 13";
            product2.Description = "128GB, Midnight";
            product2.Photo = "iphone_13.jpg";
            product2.Price = 899;
            product2.Discount = 5;

            var product3 = new Product();
            product3.Name = "Sony WH-1000XM4 Kõrvaklapid";
            product3.Description = "Juhtmeta mürasummutavad kõrvaklapid";
            product3.Photo = "sony_wh1000xm4.jpg";
            product3.Price = 299;
            product3.Discount = 15;

            var product4 = new Product();
            product4.Name = "Dell XPS 13 Sülearvuti";
            product4.Description = "13.4-tolline, Intel i7, 16GB RAM, 512GB SSD";
            product4.Photo = "dell_xps13.jpg";
            product4.Price = 1399;
            product4.Discount = 20;

            var product5 = new Product();
            product5.Name = "LG OLED55C1 55-tolline OLED TV";
            product5.Description = "4K OLED TV, HDR tugi, Smart TV";
            product5.Photo = "lg_oled55c1.jpg";
            product5.Price = 1299;
            product5.Discount = 25;

            var product6 = new Product();
            product6.Name = "Bosch Külmik";
            product6.Description = "325L, madal energiatarve, A++";
            product6.Photo = "bosch_kulmik.jpg";
            product6.Price = 599;
            product6.Discount = 0;

            var product7 = new Product();
            product7.Name = "Dyson V11 Tolmuimeja";
            product7.Description = "Juhtmeta tolmuimeja, suur imemisvõime";
            product7.Photo = "dyson_v11.jpg";
            product7.Price = 499;
            product7.Discount = 10;

            var product8 = new Product();
            product8.Name = "Canon EOS R6 Kaamera";
            product8.Description = "Täiskaader peeglita kaamera, 20MP";
            product8.Photo = "canon_eos_r6.jpg";
            product8.Price = 2399;
            product8.Discount = 15;

            var product9 = new Product();
            product9.Name = "JBL Flip 5 Kõlar";
            product9.Description = "Juhtmevaba veekindel Bluetooth kõlar";
            product9.Photo = "jbl_flip5.jpg";
            product9.Price = 119;
            product9.Discount = 5;

            var product10 = new Product();
            product10.Name = "KitchenAid Mikser";
            product10.Description = "Pro mikser, punane";
            product10.Photo = "kitchenaid_mikser.jpg";
            product10.Price = 499;
            product10.Discount = 0;

            // Lisame loodud tooted andmebaasi
            context.Products.AddRange(product1, product2, product3, product4, product5, product6, product7, product8, product9, product10);

            // Salvesta muudatused andmebaasi
            context.SaveChanges();

        }
        public static void GenerateCustomers(ApplicationDbContext context)
        {
            // Kontrollime, kas andmebaasis on juba andmeid tabelis "Customers"
            if (context.Customers.Any())
            {
                Console.WriteLine("Kliendid on juba olemas.");
                return;
            }

            // Loodud kliendid
            var customers = new List<Customer>
    {
        new Customer { Name = "Leonardo DiCaprio" },
        new Customer { Name = "Emma Watson" },
        new Customer { Name = "Will Smith" },
        new Customer { Name = "Scarlett Johansson" },
        new Customer { Name = "Tom Hanks" },
        new Customer { Name = "Meryl Streep" },
        new Customer { Name = "Dwayne Johnson" },
        new Customer { Name = "Ariana Grande" },
        new Customer { Name = "Chris Hemsworth" },
        new Customer { Name = "Rihanna" }
    };

            // Lisame loodud kliendid andmebaasi
            context.Customers.AddRange(customers);

            // Salvesta muudatused andmebaasi
            context.SaveChanges();
        }
        public static void CatalogGenerate(ApplicationDbContext context)
        {
            if (context.ProductCatalogs.Any())
            {
                return;
            }
            var catalog1 = new ProductCatalog();
            catalog1.CategoryName = "Puuviljad";
            catalog1.ProductId = 1;

            var catalog2 = new ProductCatalog();
            catalog2.CategoryName = "Köögiviljad";
            catalog2.ProductId = 2;

            var catalog3 = new ProductCatalog();
            catalog3.CategoryName = "Elektroonika";
            catalog3.ProductId = 3;

            var catalog4 = new ProductCatalog();
            catalog4.CategoryName = "Kodumasinad";
            catalog4.ProductId = 4;

            var catalog5 = new ProductCatalog();
            catalog5.CategoryName = "Mööbel";
            catalog5.ProductId = 5;

            var catalog6 = new ProductCatalog();
            catalog6.CategoryName = "Riided";
            catalog6.ProductId = 6;

            var catalog7 = new ProductCatalog();
            catalog7.CategoryName = "Sporditarbed";
            catalog7.ProductId = 7;

            var catalog8 = new ProductCatalog();
            catalog8.CategoryName = "Ilutooted";
            catalog8.ProductId = 8;

            var catalog9 = new ProductCatalog();
            catalog9.CategoryName = "Mänguasjad";
            catalog9.ProductId = 9;

            var catalog10 = new ProductCatalog();
            catalog10.CategoryName = "Köögitarbed";
            catalog10.ProductId = 10;

            // Lisame loodud tootekataloogid andmebaasi
            context.ProductCatalogs.Add(catalog1);
            context.ProductCatalogs.Add(catalog2);
            context.ProductCatalogs.Add(catalog3);
            context.ProductCatalogs.Add(catalog4);
            context.ProductCatalogs.Add(catalog5);
            context.ProductCatalogs.Add(catalog6);
            context.ProductCatalogs.Add(catalog7);
            context.ProductCatalogs.Add(catalog8);
            context.ProductCatalogs.Add(catalog9);
            context.ProductCatalogs.Add(catalog10);

            // Salvesta muudatused andmebaasi
            context.SaveChanges();

        }

    public static void OrderGenerator(ApplicationDbContext context)
        {
            if (context.Orders.Any())
            {
                return;
            }
            var tellimus1 = new Order();
            tellimus1.Status = "Tegemisel";
            tellimus1.ClientId = 1;

            var tellimus2 = new Order();
            tellimus2.Status = "Tegemisel";
            tellimus2.ClientId = 2;

            var tellimus3 = new Order();
            tellimus3.Status = "Valmis";
            tellimus3.ClientId = 3;

            var tellimus4 = new Order();
            tellimus4.Status = "Tegemisel";
            tellimus4.ClientId = 4;

            var tellimus5 = new Order();
            tellimus5.Status = "Tühistatud";
            tellimus5.ClientId = 5;

            var tellimus6 = new Order();
            tellimus6.Status = "Valmis";
            tellimus6.ClientId = 6;

            var tellimus7 = new Order();
            tellimus7.Status = "Tegemisel";
            tellimus7.ClientId = 7;

            var tellimus8 = new Order();
            tellimus8.Status = "Lõpetatud";
            tellimus8.ClientId = 8;

            var tellimus9 = new Order();
            tellimus9.Status = "Tegemisel";
            tellimus9.ClientId = 9;

            var tellimus10 = new Order();
            tellimus10.Status = "Lõpetatud";
            tellimus10.ClientId = 10;

            // Lisame loodud tellimused andmebaasi
            context.Orders.Add(tellimus1);
            context.Orders.Add(tellimus2);
            context.Orders.Add(tellimus3);
            context.Orders.Add(tellimus4);
            context.Orders.Add(tellimus5);
            context.Orders.Add(tellimus6);
            context.Orders.Add(tellimus7);
            context.Orders.Add(tellimus8);
            context.Orders.Add(tellimus9);
            context.Orders.Add(tellimus10);

            // Salvesta muudatused andmebaasi
            context.SaveChanges();
        }
    }
}
