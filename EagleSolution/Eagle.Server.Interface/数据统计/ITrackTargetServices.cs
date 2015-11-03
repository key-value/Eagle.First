using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ITrackTargetServices : IAppServices
    {
        List<ShowTrackTarget> Get(int pageNum);
        void Update(UpdateTrackTarget updateTrackTarget);
        void Delete(List<Guid> trackIdList);
        UpdateTrackTarget Get(Guid trackTargetId);
        void Vote(string userKey, Guid targetId);
    }
}