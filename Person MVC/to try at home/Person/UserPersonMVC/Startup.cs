using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserPersonMVC.Startup))]
namespace UserPersonMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
