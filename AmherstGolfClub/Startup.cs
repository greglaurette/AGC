using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmherstGolfClub.Startup))]
namespace AmherstGolfClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
