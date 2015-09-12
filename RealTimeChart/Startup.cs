using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealTimeChart.Startup))]
namespace RealTimeChart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
