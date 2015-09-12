using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RealTimeChart.Startup))]
namespace RealTimeChart
{
    public class Startup
    {
        private IAppBuilder app;

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
