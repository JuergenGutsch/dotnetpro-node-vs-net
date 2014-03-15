using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ImageGallery.Infrastructire.Data;
using ImageGallery.Infrastructire.Drawing;

namespace ImageGallery.Controllers
{
    public class ImagesController : ApiController
    {

        // GET api/images/5
        public async Task<HttpResponseMessage> Get(string id, int width, int height, bool invalid)
        {
            if (id == null) throw new ArgumentNullException("id");

            var root = HttpContext.Request.PhysicalApplicationPath;
            var combined = Path.Combine(root, "App_Data", "images", id);

            var imageData = StorageContext.Current.Images.LoadSingle(x => x.Name == id);

            if (!File.Exists(combined) || imageData == null || (!invalid && !imageData.IsValid))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "File not found.");
            }

            var sizer = new InageSizer();
            var resizeImagePath = sizer.ResizeImage(combined, width, height);

            var stream = File.OpenRead(resizeImagePath);
            var result = new HttpResponseMessage
            {
                Content = new StreamContent(stream),
                StatusCode = HttpStatusCode.OK
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(imageData.Type);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = id
            };
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }

        public async Task<HttpResponseMessage> PostFile()
        {
            // Check if the request contains multipart/form-data. 
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // TODO: Remove HttpContext
            var root = HttpContext.Request.PhysicalApplicationPath;
            var combined = Path.Combine(root, "App_Data", "Images");
            var provider = new MultipartFormDataStreamProvider(combined);

            try
            {
                var sb = new StringBuilder(); // Holds the response body 

                // Read the form data and return an async task. 
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data. 
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                    }
                }

                // This illustrates how to get the file names for uploaded files. 
                foreach (var file in provider.FileData)
                {
                    //form-data; name="file"; filename="WP_000053.jpg"
                    var fileName = file.Headers.ContentDisposition.FileName.Trim(new[] { '"' });
                    var fileType = file.Headers.ContentType.ToString();

                    var fileInfo = RenameUploadedFile(combined, fileName, file.LocalFileName);
                    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));

                    SaveImageData(fileName, fileType, fileInfo.Length);
                }
                return new HttpResponseMessage
                {
                    Content = new StringContent(sb.ToString())
                };
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private HttpContextWrapper HttpContext
        {
            get { return ControllerContext.Request.Properties["MS_HttpContext"] as HttpContextWrapper; }
        }

        private static void SaveImageData(string fileName, string fileType, long fileSize)
        {
            var imageData = new ImageData
            {
                Name = fileName,
                UploadDate = DateTime.Now,
                Type = fileType,
                Size = fileSize
            };
            StorageContext.Current.Images.SaveOnSubmit(imageData);
            StorageContext.Current.Images.SubmitChanges();
        }

        private static FileInfo RenameUploadedFile(string root, string fileName, string localFileName)
        {
            var targetPath = Path.Combine(root, fileName);
            var fileInfo = new FileInfo(localFileName);
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
            fileInfo.MoveTo(targetPath);
            return fileInfo;
        }
    }
}
