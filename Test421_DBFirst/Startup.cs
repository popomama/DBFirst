using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test421_DBFirst.Startup))]
namespace Test421_DBFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
