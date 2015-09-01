using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IHeartbeatServices))]
    public class HeartbeatServices : ApplicationServices, IHeartbeatServices
    {
        public void Add(List<HeartbeatBody> heartbeatBodys, Guid machineId)
        {
            using (var monitorContext = new MonitorContext())
            {
                foreach (var heartbeatBody in heartbeatBodys)
                {
                    var heartbeat = new Heartbeat();
                    heartbeat.ID = Guid.NewGuid();
                    heartbeat.CreateTime = DateTime.Now;
                    heartbeat.LogTime = heartbeatBody.LogTime;
                    heartbeat.MachineId = machineId;
                    heartbeat.AvgNum = heartbeatBody.AvgNum;
                    heartbeat.MaxNum = heartbeatBody.MaxNum;
                    heartbeat.HourTime = heartbeatBody.HourTime;
                    monitorContext.Heartbeats.Add(heartbeat);
                }
                monitorContext.SaveChanges();
            }
            Flag = true;
        }

        public List<ShowCpuChart> GetHeartbeatList(DateTime day, Guid treeId)
        {
            var showCpuChartList = new List<ShowCpuChart>();
            var beginTime = day.Date;
            var endTime = day.Date.AddDays(1);

            using (var monitorContext = new MonitorContext())
            {
                var tree = monitorContext.Trees.FirstOrDefault(x => x.ID == treeId);
                if (tree.Null())
                {
                    Message = "请选择正确的服务器";
                    return showCpuChartList;
                }
                var heartbeatBag = (
                    from heartbeat in monitorContext.Heartbeats
                    where heartbeat.LogTime > beginTime
                          && heartbeat.LogTime < endTime
                          && heartbeat.MachineId == treeId
                    orderby heartbeat.HourTime
                    select heartbeat).ToList();

                foreach (var heartbeat in heartbeatBag)
                {
                    var showCpuChart = new ShowCpuChart();
                    showCpuChart.HourNum = heartbeat.HourTime;
                    showCpuChart.AvgNum = heartbeat.AvgNum;
                    showCpuChart.MaxNum = heartbeat.MaxNum;
                    showCpuChartList.Add(showCpuChart);
                }
            }

            Flag = true;
            return showCpuChartList;
        }
    }
}