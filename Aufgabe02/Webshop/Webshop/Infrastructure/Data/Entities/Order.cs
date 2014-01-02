using System.Collections.Generic;
using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Order : IEntity<int>
    {
        [JsonProperty("OrderId")]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderLineItems> Lineitems { get; set; }
    }
}