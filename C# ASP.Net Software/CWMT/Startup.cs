using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CWMasterTeacher3.Startup))]
namespace CWMasterTeacher3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }



}
