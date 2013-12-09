using System.Collections.Generic;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class ArticlesModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public int SelectedCategoryId { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}