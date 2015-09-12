namespace RealTimeChart.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System.Collections.Generic;

    [HubName("chartData")]
    public class ChartDataHub : Hub
    {
        private readonly ChartData _pointer;

        public ChartDataHub() : this(ChartData.Instance)
        {
        }

        public ChartDataHub(ChartData pointer)
        {
            _pointer = pointer;
        }

        public IEnumerable<int> initData()
        {
            return _pointer.initData();
        }
    }
}
