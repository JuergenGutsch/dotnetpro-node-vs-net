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

        public ActionResult Articles(int id)
        {
            var categories = _storageContext.Categories.LoadAll();
            var products = _storageContext.Products.LoadAll(x => x.CategoryId == id);
            var model = new ArticlesModel
            {
                Categories = categories,
                Products = products,
                SelectedCategoryId = id
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