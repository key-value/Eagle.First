using System;
using System.Runtime.Remoting;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateWeekComment
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
        public WeekComment CreateWeekComment(WeekTarget target,Guid accountId)
        {
            var weekComment = new WeekComment();
            weekComment.ID = Guid.NewGuid();
            weekComment.AccountId = accountId;
            weekComment.CreateTime = DateTime.Now;
            weekComment.DepartmentId = target.DepartmentId;
            weekComment.Description = Description;
            weekComment.WeekTargetId = WeekTargetId;
            return weekComment;
        }
        public WeekComment UpdateComment(WeekComment weekComment)
        {
            weekComment.AccountId = AccountId;
            weekComment.CreateTime = DateTime.Now;
            weekComment.DepartmentId = DepartmentId;
            weekComment.Description = Description;
            weekComment.WeekTargetId = WeekTargetId;
            return weekComment;
        }

        public static UpdateWeekComment CreateUpdateWeekComment(WeekComment weekComment)
        {
            var updateWeekComment = new UpdateWeekComment();
            weekComment.ID = Guid.NewGuid();
            weekComment.Description = weekComment.Description;
            return updateWeekComment;
        }
    }
}