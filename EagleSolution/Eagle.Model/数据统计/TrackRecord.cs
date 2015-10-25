using System;

namespace Eagle.Model
{
    public class TrackRecord
    {
        public Guid ID
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public Guid TargetId
        {
            get; set;
        }

        public string AllKey
        {
            get; set;
        }

        public Guid PlanId { get; set; }
    }
}