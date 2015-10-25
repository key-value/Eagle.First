using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ITrackPlanServices:IAppServices
    {
        List<ShowTrackPlan> Get(int pageNum);
        void Update(UpdateTrackPlan updateTrackPlan);
        void Delete(List<Guid> trackIdList);
        UpdateTrackPlan Get(Guid trackPlanId);
        List<ISelectTrackPlan> Get();
        void Vote(Guid planId, Guid targetId, string userKey);
    }
}