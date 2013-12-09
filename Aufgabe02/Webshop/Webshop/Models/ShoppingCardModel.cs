using System.Collections.Generic;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
            CardItems = new List<ShoppingCartItemModel>();
        }

        public IEnumerable<ShoppingCartItemModel> CardItems { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public double SumAllItems { get; set; }
    }

    public class ShoppingCartItemModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}