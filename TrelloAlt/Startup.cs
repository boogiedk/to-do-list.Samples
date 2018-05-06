using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrelloAlt.Startup))]
namespace TrelloAlt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
