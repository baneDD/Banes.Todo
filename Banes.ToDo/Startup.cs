using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Banes.ToDo.Startup))]

namespace Banes.ToDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
