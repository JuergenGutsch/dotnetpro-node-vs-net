using System;
using Gos.SimpleObjectStore;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
    }
}