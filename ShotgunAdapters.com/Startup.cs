using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShotgunAdapters.Startup))]
namespace ShotgunAdapters
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
