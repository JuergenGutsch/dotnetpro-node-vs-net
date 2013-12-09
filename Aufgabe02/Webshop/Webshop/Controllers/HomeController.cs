using System.Linq;
using System.Web.Mvc;
using Webshop.Infrastructure.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = UnitOfWork.Current;
        }

        public ActionResult Index()
        {
            var categories = _unitOfWork.Categories.LoadAll();

            var model = new CategoriesModel
            {
                Categories = categories
            };
            return View(model);
        }

        public ActionResult Articles(int id)
        {
            var categories = _unitOfWork.Categories.LoadAll();
            var products = _unitOfWork.Products.LoadAll(x => x.CategoryId == id);
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