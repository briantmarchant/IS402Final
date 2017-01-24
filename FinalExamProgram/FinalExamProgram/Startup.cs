using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalExamProgram.Startup))]
namespace FinalExamProgram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
