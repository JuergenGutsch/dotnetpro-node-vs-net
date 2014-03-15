using System.Web.Optimization;

namespace ImageGallery
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            //,
            //            "~/Scripts/jquery-migrate-1.2.1.js"

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.*",
                        "~/Scripts/angular-route.*",
                        "~/Scripts/angular-animate.*",
                        "~/Scripts/Angular/App.js",
                        "~/Scripts/Angular/AdminController.js",
                        "~/Scripts/Angular/UploadController.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                        "~/Scripts/jquery.fancybox.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery.fancybox.css",
                      "~/Content/site.css"));
        }
    }
}
