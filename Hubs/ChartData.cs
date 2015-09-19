using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace RealTimeChart.Hubs
{
    public class ChartData
    {
        private readonly static Lazy<ChartData> _instance = new Lazy<ChartData>(() => new ChartData());
        private readonly ConcurrentQueue<int> _points = new ConcurrentQueue<int>();
        private readonly int _updateInterval = 250;     
        private Timer _timer;
        private readonly object _updatePointsLock = new object();
        private bool _updatingData = false;
        private readonly Random _updateOrNotRandom = new Random();
        private int _startPoint = 50, _minPoint = 25, _maxPoint = 99;

        private ChartData()
        {

        }

        public static ChartData Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IEnumerable<int> initData()
        {
            _points.Enqueue(_startPoint);
            _timer = new Timer(TimerCallBack, null, _updateInterval, _updateInterval);
            return _points.ToArray();
        }

        private void TimerCallBack(object state)
        {
            if (_updatingData)
            {
                return;
            }
            lock (_updatePointsLock)
            {
                if (!_updatingData)
                {
                    _updatingData = true;
        
                    if (_updateOrNotRandom.Next(0, 3) == 1)
                    {
                        BroadcastChartData(UpdateData());
                    }
                    _updatingData = false;
                }
            }
        }

        private int UpdateData()
        {
            int point = _startPoint;
            if (_points.TryDequeue(out point))
            {
                var random = new Random();
                var pos = random.NextDouble() > .51;
                var change = random.Next((int)point / 2);

                change = pos ? change : -change;
                point += change;

                if (point < _minPoint)
                {
                    point = _minPoint;
                }

                if (point > _maxPoint)
                {
                    point = _maxPoint;
                }
                _points.Enqueue(point);
            }

            return point;
        }


        private void BroadcastChartData(int point)
        {
            GetClients().updateData(point);
        }

        private static dynamic GetClients()
        {
            return GlobalHost.ConnectionManager.GetHubContext<ChartDataHub>().Clients.All;
        }
    }
}
