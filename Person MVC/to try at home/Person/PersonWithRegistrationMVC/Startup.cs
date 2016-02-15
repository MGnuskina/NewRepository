using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonWithRegistrationMVC.Startup))]
namespace PersonWithRegistrationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
