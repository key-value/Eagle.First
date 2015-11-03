using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateTrackTarget
    {
        public Guid ID
        {
            get; set;
        }

        public string NiceName
        {
            get; set;
        }

        public Guid PlanId
        {
            get; set;
        }

        public TrackTarget Create()
        {
            var trackTarget = new TrackTarget();
            trackTarget.ID = Guid.NewGuid();
            trackTarget.NiceName = NiceName;
            trackTarget.PlanId = PlanId;
            trackTarget.CreateTime = DateTime.Now;
            return trackTarget;
        }

        public TrackTarget Update(TrackTarget trackTarget)
        {
            trackTarget.NiceName = NiceName;
            return trackTarget;
        }

        public static UpdateTrackTarget CreateUpdateTrackTarget(TrackTarget trackTarget)
        {
            var updateTrackTarget = new UpdateTrackTarget();
            updateTrackTarget.ID = trackTarget.ID;
            updateTrackTarget.NiceName = trackTarget.NiceName;
            updateTrackTarget.PlanId = trackTarget.PlanId;
            return updateTrackTarget;
        }
    }
}