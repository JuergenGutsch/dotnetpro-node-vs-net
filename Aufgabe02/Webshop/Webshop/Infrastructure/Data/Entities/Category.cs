using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Category : IEntity<int>
    {
        [JsonProperty("CategoryID")]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}