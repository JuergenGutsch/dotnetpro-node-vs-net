using System;
using Gos.SimpleObjectStore;

namespace Webshop.Infrastructure.Data.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}