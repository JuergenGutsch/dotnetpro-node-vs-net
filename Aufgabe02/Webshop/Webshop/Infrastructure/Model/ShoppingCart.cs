using System.Collections.Generic;

namespace Webshop.Infrastructure.Model
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            LineItems = new List<CartLineItem>();
        }
        public ICollection<CartLineItem> LineItems { get; set; }
    }
}