using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParentsBankWebApp_Group.Startup))]
namespace ParentsBankWebApp_Group
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
