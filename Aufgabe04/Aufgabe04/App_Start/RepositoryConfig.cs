using System.Web;
using Gos.SimpleObjectStore;
using Gos.SimpleObjectStore.Providers;

namespace ImageGallery
{
    public class RepositoryConfig
    {
        public static void Configure()
        {
            ObjectStore.DataProvider = new DataProvider(HttpContext.Current.Server.MapPath("/App_Data/"));
            ObjectStore.FormatOutput = true;
        }
    }
}