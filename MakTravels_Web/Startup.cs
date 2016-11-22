using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakTravels_Web.Startup))]
namespace MakTravels_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
