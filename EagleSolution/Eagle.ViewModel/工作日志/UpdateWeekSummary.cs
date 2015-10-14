using System;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateWeekSummary
    {

        public Guid ID
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }


        public WeekSummary CreateSummary(int weekNum)
        {
            var weekSummary = new WeekSummary();
            weekSummary.ID = Guid.NewGuid();
            weekSummary.CreateTime = DateTime.Now;
            weekSummary.DepartmentId = DepartmentId;
            weekSummary.Description = Description;
            weekSummary.WeekNum = weekNum;
            return weekSummary;
        }

        public static UpdateWeekSummary CreateSummary(Guid? weekId, WeekSummary weekSummary)
        {
            var updateWeekSummary = new UpdateWeekSummary();
            updateWeekSummary.ID = weekId.GetValueOrDefault();
            if (!weekSummary.Null())
            {
                updateWeekSummary.Description = weekSummary.Description;
                updateWeekSummary.DepartmentId = weekSummary.DepartmentId;
            }
            return updateWeekSummary;
        }
    }
}