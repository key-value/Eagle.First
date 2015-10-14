using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class WeekSummary : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public int WeekNum
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }

    }
}