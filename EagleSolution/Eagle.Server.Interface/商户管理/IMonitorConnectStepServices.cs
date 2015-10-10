using System;
using System.Collections.Generic;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IMonitorConnectStepServices : IAppServices
    {
        List<ShowStep> GetMonitorConnectSteps(Guid restaurantId, int pageNum);
        void UpdateConnectStep(UpdateConnectStep updateConnectStep);
        void RefreshRestaurantState(List<Guid> restIds);
    }
}