using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class WeekComment : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public Guid WeekTargetId
        {
            get; set;
        }

        public Guid AccountId
        {
            get; set;
        }

        public Guid DepartmentId
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
    }
}