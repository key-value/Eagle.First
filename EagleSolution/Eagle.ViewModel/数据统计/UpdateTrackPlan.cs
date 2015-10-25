using System;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateTrackPlan
    {
        public Guid ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime BeginTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public TrackPlan Create()
        {
            var trackPlan = new TrackPlan();
            trackPlan.ID = Guid.NewGuid();
            trackPlan.BeginTime = BeginTime.DefaultValue();
            trackPlan.EndTime = EndTime.DefaultValue();
            trackPlan.Name = Name;
            trackPlan.CreateTime = DateTime.Now;
            return trackPlan;
        }

        public TrackPlan Update(TrackPlan trackPlan)
        {
            trackPlan.BeginTime = BeginTime.DefaultValue(trackPlan.BeginTime);
            trackPlan.EndTime = EndTime.DefaultValue(trackPlan.EndTime);
            trackPlan.Name = Name;
            return trackPlan;
        }

        public static UpdateTrackPlan CreateUpdateTrackPlan(TrackPlan trackPlan)
        {
            var updateTrackPlan = new UpdateTrackPlan();
            updateTrackPlan.ID = trackPlan.ID;
            updateTrackPlan.Name = trackPlan.Name;
            updateTrackPlan.BeginTime = trackPlan.BeginTime;
            updateTrackPlan.EndTime = trackPlan.EndTime;
            return updateTrackPlan;
        }
    }
}