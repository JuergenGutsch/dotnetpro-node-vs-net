using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Product : IEntity
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }

        protected bool Equals(Product other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}