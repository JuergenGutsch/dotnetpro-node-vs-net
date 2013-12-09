using System;
using Gos.SimpleObjectStore;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
    }
}