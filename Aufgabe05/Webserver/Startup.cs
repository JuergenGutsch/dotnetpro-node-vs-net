using System;
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

            app.Use(typeof(GitReceivePackMiddleware), webserverOptions);
            app.Use(typeof(IsPrivateFolderMiddleware), webserverOptions);
            app.Use(typeof(ResourceExistMiddleware), webserverOptions);
            app.Use(typeof(DefaultMiddleware), webserverOptions);
        }

    }

}