using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vinodsharma.Startup))]
namespace vinodsharma
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
