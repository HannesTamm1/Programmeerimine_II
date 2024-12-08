﻿namespace KooliProjekt.Data
{
    public class Category : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
