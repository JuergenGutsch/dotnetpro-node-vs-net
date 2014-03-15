using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using Gos.SimpleObjectStore;
using ImageGallery.Infrastructire.Data;

namespace ImageGallery.Controllers
{
    public class MetadataController : ApiController
    {
        // GET api/imagedata
        public IEnumerable<ImageData> GetValidImages()
        {
            using (var store = ObjectStore.GetInstance<ImageData>())
            {
                return store.LoadAll(x => x.IsValid);
            }
        }
        // GET api/imagedata
        public IEnumerable<ImageData> GetInvalidImages()
        {
            using (var store = ObjectStore.GetInstance<ImageData>())
            {
                return store.LoadAll(x => !x.IsValid);
            }
        }

        // GET api/imagedata/5
        public ImageData Get(string id)
        {
            return new ImageData();
        }

        // POST api/imagedata
        public void Post(ImageData image)
        {
            using (var store = ObjectStore.GetInstance<ImageData>())
            {
                    image.IsValid = true;
                    store.SaveOnSubmit(image);
            }
        }

        // PUT api/imagedata/5
        public void Put(string id, ImageData value)
        {
            using (var store = ObjectStore.GetInstance<ImageData>())
            {
                value.IsValid = true;
                store.SaveOnSubmit(value);

            }
        }

        // DELETE api/imagedata/5
        public void Delete(string id)
        {
            using (var store = ObjectStore.GetInstance<ImageData>())
            {
                store.DeleteOnSubmit(x => x.Name == id);
                var root = HttpContext.Request.PhysicalApplicationPath;

                var imagePath = Path.Combine(root, "App_Data", "Images", id);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

        private HttpContextWrapper HttpContext
        {
            get { return ControllerContext.Request.Properties["MS_HttpContext"] as HttpContextWrapper; }
        }
    }
}
