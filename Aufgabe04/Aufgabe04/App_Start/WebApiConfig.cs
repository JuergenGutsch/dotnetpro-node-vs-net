using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ImageGallery
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/Images/{id}/{width}/{height}/{invalid}",
                defaults: new {controller="Images", id = RouteParameter.Optional, width = 0, height = 0, invalid = false }
            );

            config.Routes.MapHttpRoute("DefaultInvalid", "api/Metadata/Invalid", new { controller = "Metadata", action = "GetInvalidImages" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiGet", "api/Metadata", new { controller = "Metadata", action = "GetValidImages" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", "api/Metadata", new { controller = "Metadata", action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiDelete", "api/Metadata/{id}", new { controller = "Metadata", action = "Delete", id = RouteParameter.Optional }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });
        }
    }
}
