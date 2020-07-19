using System;
using System.Threading.Tasks;
using Library.Implementation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //AutoMapperConfig.Configure();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
