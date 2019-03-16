using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Group7A2.Startup))]
namespace Group7A2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
