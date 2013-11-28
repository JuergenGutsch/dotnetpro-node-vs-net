using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Owin;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    class DownloadMiddlewar : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public DownloadMiddlewar(OwinMiddleware next, WebserverOptions options)
            : base(next)
        {
            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {
            var path = FileSystem.MapPath(_options.BasePath, context.Request.Path.Value);
            var ext = Path.GetExtension(path);
            if (CanHandle(ext, context.Request.Path.Value))
            {
                Trace.WriteLine("Invoke DownloadMiddlewar");

                var filename = Path.GetFileName(path);
                var folderPath = Path.Combine(_options.BasePath, "downloads");

                context.Response.StatusCode = 200;
                context.Response.ContentType = MimeTypeService.GetMimeType(".zip");
                context.Response.Headers.Add("Content-Disposition", new[] { String.Format("attachment; filename={0}.zip", filename) });

                var fastZip = new FastZip();
                fastZip.CreateZip(context.Response.Body, folderPath, false, filename, null);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        private static bool CanHandle(string ext, string path)
        {
            return !String.IsNullOrWhiteSpace(ext) && !ext.Equals(".zip") && path.StartsWith("/downloads/");
        }
    }
}
