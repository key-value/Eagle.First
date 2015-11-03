using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowTrackPlan : ISelectTrackPlan
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

        public static ShowTrackPlan CreateShowTrackPlan(TrackPlan trackPlan)
        {
            var showTrackPlan = new ShowTrackPlan();
            showTrackPlan.ID = trackPlan.ID;
            showTrackPlan.Name = trackPlan.Name;
            showTrackPlan.BeginTime = trackPlan.BeginTime;
            showTrackPlan.EndTime = trackPlan.EndTime;
            return showTrackPlan;
        }
    }
}