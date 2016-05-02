using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coda.Startup))]
namespace Coda
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
