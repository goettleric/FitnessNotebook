using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessNotebook.Startup))]
namespace FitnessNotebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
