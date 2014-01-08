using System;
using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Category : IEntity
    {
        [JsonProperty("CategoryID")]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Category) obj);
        }
        protected bool Equals(Category other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}