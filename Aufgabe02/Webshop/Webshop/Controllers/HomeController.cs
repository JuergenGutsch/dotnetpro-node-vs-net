using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Webshop.Infrastructure.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly StorageContext _storageContext;

        public HomeController()
        {
            _storageContext = StorageContext.Current;
        }

        public ActionResult Index()
        {
            var categories = _storageContext.Categories.LoadAll();

            var model = new CategoriesModel
            {
                Categories = categories
            };
            return View(model);
        }

        public ActionResult Articles(int id, int page)
        {
            var categories = _storageContext.Categories.LoadAll();
            var products = _storageContext.Products.LoadAll(x => x.CategoryId == id);
            var model = new ArticlesModel
            {
                Categories = categories,
                CurrentPage = page,
                Pages = GetPages(products),
                Products = products.Skip(page * 5).Take(5),
                SelectedCategoryId = id
            };

            return View(model);
        }

        private int GetPages<T>(IEnumerable<T> list)
        {
            var fullPages = list.Count()/5;
            if ((list.Count()%5) > 0)
            {
                return fullPages + 1;
            }
            return fullPages;

        }

        public ActionResult Article(int id)
        {
            var categories = _storageContext.Categories.LoadAll();
            var product = _storageContext.Products.LoadSingle(x => x.Id == id);

            var model = new ArticleModel
            {
                Categories = categories,
                Product = product,
                SelectedCategoryId = product.CategoryId
            };

            return View(model);
        }

        public ActionResult Warenkorb()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}