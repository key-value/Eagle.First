using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowWeekComent
    {

        public Guid ID
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string CreateDay
        {
            get; set;
        }

        public string CreateTime
        {
            get; set;
        }

        public int ConnectType
        {
            get; set;
        }

        public static ShowWeekComent CreateWeekComent(WeekComment weekComment)
        {
            var showWeekComent = new ShowWeekComent();
            showWeekComent.ID = weekComment.ID;
            showWeekComent.Description = weekComment.Description;
            showWeekComent.CreateTime = weekComment.CreateTime.ToString("HH:mm");
            showWeekComent.CreateDay = weekComment.CreateTime.ToString("yy-MM-dd ");
            showWeekComent.ConnectType = 1;
            return showWeekComent;
        }
    }
}