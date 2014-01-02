using System.Linq;
using System.Web.Mvc;
using Webshop.Infrastructure;
using Webshop.Infrastructure.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class CartController : Controller
    {
        private readonly StorageContext _storageContext;

        public CartController()
        {
            _storageContext = StorageContext.Current;
        }

        public ActionResult Index()
        {
            var session = new SessionFacade(HttpContext.Session);
            var categories = _storageContext.Categories.LoadAll();

            var items = session.ShoppingCart.LineItems.Select(x => new ShoppingCartItemModel
            {
                Product = _storageContext.Products.LoadSingle(y => y.Id == x.ProductId),
                Quantity = x.Quantity
            });

            var model = new ShoppingCartModel
            {
                CardItems = items,
                Categories = categories,
                SumAllItems = items.Select(x => x.Product.UnitPrice * x.Quantity).Sum()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(UpdateCardModel model)
        {
            var cart = new Cart(new SessionFacade(HttpContext.Session), _storageContext);
            
            cart.UpdateCard(model.Id, model.Quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCard(UpdateCardModel model)
        {
            var cart = new Cart(new SessionFacade(HttpContext.Session), _storageContext);

            cart.AddToCard(model.Id, model.Quantity);

            return RedirectToAction("Index");
        }
    }
}