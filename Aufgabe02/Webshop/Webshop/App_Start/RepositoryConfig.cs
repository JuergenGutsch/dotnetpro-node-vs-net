using System.Web;
using Gos.SimpleObjectStore;
using Gos.SimpleObjectStore.Providers;

namespace Webshop.App_Start
{
    public class RepositoryConfig
    {
        public static void Config(HttpServerUtility server)
        {
            ObjectStore.DataProvider = new DataProvider(server.MapPath("/App_Data/"));
            ObjectStore.FormatOutput = true;
        }
    }
}