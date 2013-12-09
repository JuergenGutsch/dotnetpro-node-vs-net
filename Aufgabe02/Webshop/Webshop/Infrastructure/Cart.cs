using System.Linq;
using Webshop.Infrastructure.Model;

namespace Webshop.Infrastructure
{
    public class Cart
    {
        private readonly SessionFacade _session;

        public Cart(SessionFacade session)
        {
            _session = session;
        }

        public void AddToCard(int productId, int quantity)
        {
            var shoppingCart = _session.ShoppingCart;
            if (shoppingCart.LineItems.Any(x => x.ProductId == productId))
            {
                var item = shoppingCart.LineItems.First(x => x.ProductId == productId);
                item.Quantity += quantity;
            }
            else
            {
                shoppingCart.LineItems.Add(new CartLineItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            _session.ShoppingCart = shoppingCart;
        }
    }
}