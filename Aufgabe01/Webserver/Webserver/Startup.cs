using System;
using System.Collections.Generic;
using Owin;
using Webserver.Middleware;
using Webserver.Options;

namespace Webserver
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webserverOptions = new WebserverOptions
            {
                BasePath = Environment.CurrentDirectory
            };

            app.Use(typeof(TraceMiddleware));
            app.UseHandlerAsync((request, response, next) =>
            {
                response.AddHeader("Server", "dotnetpro Webserver");
                return next();
            });

            app.Use(typeof(IsPrivateFolderMiddleware), webserverOptions);
            app.Use(typeof(ResourceExistMiddleware), webserverOptions);
            app.Use(typeof(DownloadMiddlewar), webserverOptions);
            app.Use(typeof(LessMiddleware), webserverOptions);
            app.Use(typeof(SassMiddleware), webserverOptions);
            app.Use(typeof(MinifyMiddleware), webserverOptions);
            app.Use(typeof(RazorMiddleware), webserverOptions);
            app.Use(typeof(DefaultMiddleware), webserverOptions);
        }



        private void RegisterAddresses(IAppBuilder app)
        {

            var hostUri = new Uri("http://localhost:16897/");
            var hostUriHttps = new Uri("https://localhost:44567/");

            // NOTE: OwinServerFactoryAttribute.Create method and 
            //       OwinHttpListener.Start should give you a hint on how
            //       to register the addresses.
            app.Properties["host.Addresses"] = new List<IDictionary<string, object>> {
            new Dictionary<string, object> { 
                { "scheme", hostUri.Scheme },
                { "host", hostUri.Host },
                { "port", hostUri.Port.ToString()  }
            },

            new Dictionary<string, object> { 
                { "scheme", hostUriHttps.Scheme },
                { "host", hostUriHttps.Host },
                { "port", hostUriHttps.Port.ToString()  }
            }
        };
        }

    }

}