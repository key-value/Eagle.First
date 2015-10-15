using System;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowWeeklyPlan
    {
        public Guid ID
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string CreateTime
        {
            get; set;
        }

        public int TargetTime
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }

        public string DepartmentName
        {
            get; set;
        }

        public string Target
        {
            get; set;
        }

        public int Progress
        {
            get; set;
        }

        public static ShowWeeklyPlan CreateShowWeeklyPlan(WeekSummary weekSummary, WeekTarget weekTarget, Department department)
        {
            var showWeeklyPlan = new ShowWeeklyPlan();
            if (!weekSummary.Null())
            {
                showWeeklyPlan.ID = weekSummary.ID;
                showWeeklyPlan.Description = weekSummary.Description;
                showWeeklyPlan.CreateTime = weekSummary.CreateTime.ToString("yy-MM-dd HH:mm");
                showWeeklyPlan.TargetTime = weekSummary.WeekNum;
            }
            if (!weekTarget.Null())
            {
                showWeeklyPlan.Target = weekTarget.Target ?? " ";
                showWeeklyPlan.Progress = weekTarget.Progress;
                showWeeklyPlan.TargetTime = weekTarget.WeekNum;
            }
            if (!department.Null())
            {
                showWeeklyPlan.DepartmentName = department.Name;
            }
            return showWeeklyPlan;
        }

    }
}