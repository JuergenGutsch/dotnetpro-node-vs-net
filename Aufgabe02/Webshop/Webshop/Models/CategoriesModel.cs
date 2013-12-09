using System.Collections.Generic;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class CategoriesModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}