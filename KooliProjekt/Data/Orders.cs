using System.Numerics;

namespace KooliProjekt.Data
{
    public class Orders
    {
        public int id { get; set; }
        public DateTime LineItem { get; set; }
        public int user_id { get; set; }
        public string status { get; set; }
    }
}
