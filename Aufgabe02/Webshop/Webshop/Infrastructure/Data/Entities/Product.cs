using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Product : IEntity<int>
    {
        [JsonProperty("ProductID")]
        public int Id { get; set; }

        public string ProductName { get; set; }

        [JsonProperty("CategoryID")]
        public int CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }
    }
}