using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fondo9Sa3ada.Startup))]
namespace fondo9Sa3ada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
