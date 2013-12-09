using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Webshop.Infrastructure;
using Webshop.Infrastructure.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class CartController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CartController()
        {
            _unitOfWork = UnitOfWork.Current;
        }

        public ActionResult Index()
        {
            var session = new SessionFacade(HttpContext.Session);
            var categories = _unitOfWork.Categories.LoadAll();

            var items = session.ShoppingCart.LineItems.Select(x => new ShoppingCartItemModel
            {
                Product = _unitOfWork.Products.LoadSingle(y => y.Id == x.ProductId),
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
            var cart = new Cart(new SessionFacade(HttpContext.Session));
            var product = _unitOfWork.Products.LoadSingle(x => x.Id == model.Id);
            if (product.UnitsInStock < model.Quantity)
            {
                // handle stock error
            }

            // cart.UpdateCard(model.Id, model.Quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCard(UpdateCardModel model)
        {

            var cart = new Cart(new SessionFacade(HttpContext.Session));
            var product = _unitOfWork.Products.LoadSingle(x => x.Id == model.Id);
            if (product.UnitsInStock < model.Quantity)
            {
                // handle stock error
            }

            cart.AddToCard(model.Id, model.Quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCart()
        {

            return RedirectToAction("Index");
        }
    }
}