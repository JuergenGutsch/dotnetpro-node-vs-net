using System.Linq;
using Webshop.Infrastructure.Data;
using Webshop.Infrastructure.Data.Entities;
using Webshop.Infrastructure.Model;

namespace Webshop.Infrastructure
{
    public class Cart
    {
        private readonly SessionFacade _session;
        private readonly StorageContext _storageContext;

        public Cart(SessionFacade session, StorageContext storageContext)
        {
            _session = session;
            _storageContext = storageContext;
        }

        public void AddToCard(int productId, int quantity)
        {
            var product = _storageContext.Products.LoadSingle(x => x.Id == productId);
            
            var shoppingCart = _session.ShoppingCart;
            if (shoppingCart.LineItems.Any(x => x.ProductId == productId))
            {
                var item = shoppingCart.LineItems.First(x => x.ProductId == productId);
                item.Quantity += quantity;
                // handle stock error
                if (item.Quantity > product.UnitsInStock)
                {
                    item.Quantity = product.UnitsInStock;
                }
            }
            else
            { 
                // handle stock error
                if (quantity > product.UnitsInStock)
                {
                    quantity = product.UnitsInStock;
                }

                shoppingCart.LineItems.Add(new CartLineItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            _session.ShoppingCart = shoppingCart;
        }

        public void UpdateCard(int productId, int quantity)
        {
            var product = _storageContext.Products.LoadSingle(x => x.Id == productId);
            
            var shoppingCart = _session.ShoppingCart;
            var item = shoppingCart.LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null && quantity <= 0)
            {
                shoppingCart.LineItems.Remove(item);
                _session.ShoppingCart = shoppingCart;
            }

            if (item != null && quantity > 0)
            {
                // handle stock error
                if (quantity > product.UnitsInStock)
                {
                    quantity = product.UnitsInStock;
                }

                item.Quantity = quantity;
                _session.ShoppingCart = shoppingCart;
            }
        }

        public Order GetOrder()
        {
            var shoppingCart = _session.ShoppingCart;
            var lineItems = shoppingCart.LineItems.Select(x=> new OrderLineItems
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            });

            var order = new Order
            {
                Lineitems = lineItems
            };

            return order;
        }
    }

    public class OrderLineItems
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}