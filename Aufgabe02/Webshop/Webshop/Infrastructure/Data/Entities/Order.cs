using System.Collections.Generic;
using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Order : IEntity
    {
        [JsonProperty("OrderId")]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<OrderLineItems> Lineitems { get; set; }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Order)obj);
        }
        protected bool Equals(Order other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}