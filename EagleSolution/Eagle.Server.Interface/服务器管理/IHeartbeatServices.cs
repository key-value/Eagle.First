using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IHeartbeatServices : IAppServices
    {
        void Add(List<HeartbeatBody> heartbeatBodys, Guid machineId);
    }
}