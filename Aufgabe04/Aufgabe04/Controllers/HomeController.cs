using System.Collections.Generic;
using System.Web.Mvc;
using Gos.SimpleObjectStore;
using ImageGallery.Infrastructire.Data;
using ImageGallery.Models;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
{
public ActionResult Index()
{
    IEnumerable<ImageData> images;
    using (var store = ObjectStore.GetInstance<ImageData>())
    {
        images = store.LoadAll(x => x.IsValid);
    }

    var model = new ImageGaleryModel
    {
        Images = images
    };

    return View(model);
}
    }
}