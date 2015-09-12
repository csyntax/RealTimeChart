using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RealTimeChart.Startup))]
namespace RealTimeChart
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
