using System.Web.Http;

namespace BooksCollector.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RepositoryConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
