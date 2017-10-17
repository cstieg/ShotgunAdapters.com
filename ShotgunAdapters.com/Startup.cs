using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShotgunAdapters.com.Startup))]
namespace ShotgunAdapters.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
