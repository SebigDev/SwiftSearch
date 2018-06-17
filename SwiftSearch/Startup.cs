using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwiftSearch.Startup))]
namespace SwiftSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
