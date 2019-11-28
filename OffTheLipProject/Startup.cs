using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OffTheLipProject.Startup))]
namespace OffTheLipProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
