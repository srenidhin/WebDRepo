using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaawinPages.Startup))]
namespace MaawinPages
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
