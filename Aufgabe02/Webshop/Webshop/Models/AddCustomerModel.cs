using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class AddCustomerModel
    {
        public int CustomerId { get; set; }

        [Required]
        public string Salutation { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}