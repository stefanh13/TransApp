using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransApp.Startup))]
namespace TransApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
