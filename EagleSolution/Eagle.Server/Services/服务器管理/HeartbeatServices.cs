using System;
using System.Collections.Generic;
using Eagle.Domain.EF;
using Eagle.Infrastructrue.Aop.Attribute;
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
    }
}