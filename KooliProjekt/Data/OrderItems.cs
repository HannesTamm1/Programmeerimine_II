namespace KooliProjekt.Data
{
    public class OrderItems
    {
            public int id { get; set; }
            public int order_id { get; set; }
            public int product_id { get; set; }
            public float price_at_order { get; set; }
            public int quantity { get; set; }
    }
}
