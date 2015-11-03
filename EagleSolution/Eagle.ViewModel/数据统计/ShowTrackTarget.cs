using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowTrackTarget
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

        public string PlanName
        {
            get; set;
        }

        public static ShowTrackTarget CreateShowTrackTarget(TrackTarget arg, List<TrackPlan> trackPlans)
        {
            var showTrackTarget = new ShowTrackTarget();
            showTrackTarget.ID = arg.ID;
            showTrackTarget.NiceName = arg.NiceName;
            showTrackTarget.PlanId = arg.PlanId;
            var trackPlan = trackPlans.FirstOrDefault(x => x.ID == arg.PlanId);
            if (!trackPlan.Null())
            {
                showTrackTarget.PlanName = trackPlan.Name;
            }
            return showTrackTarget;


        }
    }
}