using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class WeekTarget : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public string Target
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }

        public int Progress
        {
            get; set;
        }

        public Guid AccountId
        {
            get; set;
        }

        public int WeekNum
        {
            get; set;
        }

    }
}