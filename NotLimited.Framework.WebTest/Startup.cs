using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NotLimited.Framework.WebTest.Startup))]
namespace NotLimited.Framework.WebTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
