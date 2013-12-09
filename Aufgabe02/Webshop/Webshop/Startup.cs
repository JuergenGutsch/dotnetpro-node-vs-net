using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webshop.Startup))]
namespace Webshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
