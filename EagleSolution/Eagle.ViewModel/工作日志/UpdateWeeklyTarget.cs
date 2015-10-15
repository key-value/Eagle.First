using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateWeeklyTarget
    {
        public Guid ID
        {
            get; set;
        }

        public string Target
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

        public WeekTarget CreateWeekTarget(int weekNum)
        {
            var weekTarget = new WeekTarget();
            weekTarget.ID = Guid.NewGuid();
            weekTarget.Target = Target;
            weekTarget.DepartmentId = DepartmentId;
            weekTarget.AccountId = AccountId;
            weekTarget.CreateTime = DateTime.Now;
            weekTarget.Progress = 100;
            weekTarget.WeekNum = weekNum;
            return weekTarget;
        }

        public WeekTarget UpdateWeekTarget(WeekTarget weekTarget)
        {
            weekTarget.Target = Target;
            //weekTarget.Progress = Progress;
            return weekTarget;
        }

        public static UpdateWeeklyTarget CreateWeeklyTarget(WeekTarget weekTarget)
        {
            var updateWeeklyTarget = new UpdateWeeklyTarget();
            updateWeeklyTarget.ID = weekTarget.ID;
            updateWeeklyTarget.Target = weekTarget.Target;
            updateWeeklyTarget.Progress = weekTarget.Progress;

            return updateWeeklyTarget;
        }
    }
}