using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeopleApp.Startup))]
namespace PeopleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
