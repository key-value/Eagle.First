using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class TrackPlan : IEntity
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

        public DateTime CreateTime
        {
            get; set;
        }
    }
}