using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IWeeklyPlanServices))]
    public class WeeklyPlanServices : ApplicationServices, IWeeklyPlanServices
    {
        public ShowWeeklyPlan GetLastWeekPlan(Guid accountId)
        {
            var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Now);
            using (var workContext = new DefaultContext())
            {
                var workCard = (
                    from card in workContext.WorkCards
                    join department in workContext.Departments on card.DepartmentId equals department.ID
                    where card.AccountId == accountId
                    select new
                    {
                        card,
                        department
                    }).FirstOrDefault();

                if (workCard.Null())
                {
                    return new ShowWeeklyPlan();
                }
                var weeklyPaln = (
                    from target in workContext.WeekTargets
                    let summ = workContext.WeekSummaries.FirstOrDefault(x => x.ID == target.ID)
                    where target.DepartmentId == workCard.card.DepartmentId && target.WeekNum == weekNum
                    select new
                    {
                        target,
                        summ
                    }).FirstOrDefault();
                if (weeklyPaln.Null())
                {
                    var target = new WeekTarget();
                    target.ID = Guid.NewGuid();
                    target.CreateTime = DateTime.Now;
                    target.DepartmentId = (Guid)workCard?.department.ID;
                    target.WeekNum = weekNum;
                    target.Target = "本周日志还没写！！！";
                    target.Progress = 100;
                    workContext.WeekTargets.Add(target);
                    workContext.SaveChanges();
                }
                return ShowWeeklyPlan.CreateShowWeeklyPlan(weeklyPaln?.summ, weeklyPaln?.target,
                    workCard?.department);
            }
        }

        public List<ShowWeeklyPlan> Get(Guid weekPlanId, int pageNum)
        {
            var showWeeklyPlans = new List<ShowWeeklyPlan>();
            using (var workContext = new DefaultContext())
            {
                var workCard = (
                               from card in workContext.WorkCards
                               join department in workContext.Departments on card.DepartmentId equals department.ID
                               where card.AccountId == weekPlanId
                               select new
                               {
                                   card,
                                   department
                               }).FirstOrDefault();

                if (workCard.Null())
                {
                    return new List<ShowWeeklyPlan>();
                }

                var preWeeklyPalns = (from target in workContext.WeekTargets
                                      let summ = workContext.WeekSummaries.FirstOrDefault(x => x.ID == target.ID)
                                      where target.DepartmentId == workCard.card.DepartmentId
                                      orderby target.WeekNum descending
                                      select new
                                      {
                                          target,
                                          summ
                                      }).Pageing(pageNum, PageSize, ref _pageCount);
                showWeeklyPlans.AddRange(preWeeklyPalns.Select(x => ShowWeeklyPlan.CreateShowWeeklyPlan(x?.summ, x?.target, workCard?.department)));
            }
            return showWeeklyPlans;
        }



        public List<ShowWeeklyPlan> Get(int pageNum)
        {
            var showWeeklyPlans = new List<ShowWeeklyPlan>();

            using (var workContext = new DefaultContext())
            {
                var preWeeklyPalns = (from target in workContext.WeekTargets
                                      let summ = workContext.WeekSummaries.FirstOrDefault(x => x.ID == target.ID)
                                      join department in workContext.Departments on target.DepartmentId equals department.ID
                                      select new
                                      {
                                          target,
                                          summ,
                                          department
                                      }).GroupBy(x => x.target.WeekNum).OrderByDescending(x => x.Key).Pageing(pageNum, ref _pageCount).FirstOrDefault().ToList();
                showWeeklyPlans.AddRange(preWeeklyPalns.Select(x => ShowWeeklyPlan.CreateShowWeeklyPlan(x?.summ, x?.target, x.department)));
            }
            return showWeeklyPlans;
        }

        public UpdateWeeklyTarget Get(Guid accountId)
        {
            var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Now);
            var updateWeeklyTarget = new UpdateWeeklyTarget();
            using (var workContext = new DefaultContext())
            {
                var workCard = (
                    from card in workContext.WorkCards
                    join department in workContext.Departments on card.DepartmentId equals department.ID
                    where card.AccountId == accountId
                    select new
                    {
                        card,
                        department
                    }).FirstOrDefault();

                if (workCard.Null())
                {
                    return updateWeeklyTarget;
                }
                var weeklyPaln = (
                    from target in workContext.WeekTargets
                    let summ = workContext.WeekSummaries.FirstOrDefault(x => x.ID == target.ID)
                    where target.DepartmentId == workCard.card.DepartmentId && target.WeekNum == weekNum
                    select new
                    {
                        target,
                        summ
                    }).FirstOrDefault();
                updateWeeklyTarget = UpdateWeeklyTarget.CreateWeeklyTarget(weeklyPaln?.target);
                updateWeeklyTarget.AccountId = accountId;
                updateWeeklyTarget.DepartmentId = workCard.department.ID;
                return updateWeeklyTarget;
            }
        }

        public UpdateWeekSummary GetWeekSummary(Guid accountId)
        {
            var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Now);
            var updateWeekSummary = new UpdateWeekSummary();
            using (var workContext = new DefaultContext())
            {
                var workCard = (
                    from card in workContext.WorkCards
                    join department in workContext.Departments on card.DepartmentId equals department.ID
                    where card.AccountId == accountId
                    select new
                    {
                        card,
                        department
                    }).FirstOrDefault();

                if (workCard.Null())
                {
                    return updateWeekSummary;
                }
                var weeklyPaln = (
                    from target in workContext.WeekTargets
                    let summ = workContext.WeekSummaries.FirstOrDefault(x => x.ID == target.ID)
                    where target.DepartmentId == workCard.card.DepartmentId && target.WeekNum == weekNum
                    select new
                    {
                        target,
                        summ
                    }).FirstOrDefault();
                updateWeekSummary = UpdateWeekSummary.CreateSummary(weeklyPaln?.target.ID, weeklyPaln?.summ);

                return updateWeekSummary;
            }
        }

        public void Update(UpdateWeeklyTarget updateWeeklyTarget, Guid userId)
        {
            using (var workContext = new DefaultContext())
            {
                var workCard = workContext.WorkCards.FirstOrDefault(x => x.AccountId == userId);
                if (workCard.Null())
                {
                    Message = "账号没有部门";
                    return;
                }
                updateWeeklyTarget.DepartmentId = workCard.DepartmentId;
                updateWeeklyTarget.AccountId = userId;

                var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Today);
                var dayOfWeek = DateTime.Now.DayOfWeek;
                var weekTarget = workContext.WeekTargets.FirstOrDefault(x => x.WeekNum == weekNum && x.DepartmentId == updateWeeklyTarget.DepartmentId);
                if (weekTarget.Null())
                {
                    Message = "修改日志不存在";
                    return;
                }
                if (((dayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour > 11) || dayOfWeek > DayOfWeek.Monday))
                {
                    Message = "周计划目标已经锁定，无法修改";
                    //Message = "周记已经被锁定无法总结";
                    return;
                }
                weekTarget = updateWeeklyTarget.UpdateWeekTarget(weekTarget);
                workContext.ModifiedModel(weekTarget);
                workContext.SaveChanges();
                Flag = true;
            }
        }

        //private void AddTarget(UpdateWeeklyTarget updateWeeklyTarget)
        //{
        //    var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Today);
        //    using (var workContext = new DefaultContext())
        //    {
        //        var weekTarget = workContext.WeekTargets.FirstOrDefault(x => x.WeekNum == weekNum && x.DepartmentId == updateWeeklyTarget.DepartmentId);
        //        if (!weekTarget.Null())
        //        {
        //            Message = "本周已经有日志";
        //            return;
        //        }
        //        weekTarget = updateWeeklyTarget.CreateWeekTarget(weekNum);
        //        workContext.WeekTargets.Add(weekTarget);
        //        Flag = true;
        //    }
        //}

        //private void EditTarget(UpdateWeeklyTarget updateWeeklyTarget)
        //{
        //    var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Today);


        //    using (var workContext = new DefaultContext())
        //    {
        //    }
        //}


        public void UpdateSummary(UpdateWeekSummary updateWeekSummary)
        {
            using (var workContext = new DefaultContext())
            {
                var weekTarget = workContext.WeekTargets.FirstOrDefault(x => x.ID == updateWeekSummary.ID);
                if (weekTarget.Null())
                {
                    Message = "总结周记不存在";
                    return;
                }
                var weekNum = DateTimeUtility.GetWeekOfYear(DateTime.Now);
                if (weekNum > weekTarget.WeekNum + 1)
                {
                    if (DateTime.Now.Hour > 2 || DateTime.Today.DayOfWeek > DayOfWeek.Monday)
                    {
                        Message = "周记已经被锁定无法总结!";
                        return;
                    }
                }
                var weekSummary = workContext.WeekSummaries.FirstOrDefault(x => x.ID == updateWeekSummary.ID);
                if (weekSummary.Null())
                {
                    weekSummary = new WeekSummary();
                    weekSummary.ID = updateWeekSummary.ID;
                    weekSummary.CreateTime = DateTime.Now;
                    weekSummary.DepartmentId = updateWeekSummary.DepartmentId;
                    weekSummary.WeekNum = weekTarget.WeekNum;
                    weekSummary.DepartmentId = weekTarget.DepartmentId;
                    workContext.WeekSummaries.Add(weekSummary);
                }
                else
                {
                    workContext.ModifiedModel(weekSummary);
                }

                weekSummary.Description = updateWeekSummary.Description;
                workContext.SaveChanges();
                Flag = true;
            }
        }

        public void UpdateComment(UpdateWeekComment updateWeekComment, Guid accountId)
        {
            if (updateWeekComment.ID.Null())
            {
                AddComment(updateWeekComment, accountId);
            }
            else
            {
                EditComment(updateWeekComment);
            }
        }

        private void AddComment(UpdateWeekComment updateWeekComment, Guid accountId)
        {
            using (var workContext = new DefaultContext())
            {
                var weekTarget = workContext.WeekTargets.FirstOrDefault(x => x.ID == updateWeekComment.WeekTargetId);
                if (weekTarget.Null())
                {
                    Message = "评论日志不存在";
                    return;
                }
                var weekComment = updateWeekComment.CreateWeekComment(weekTarget, accountId);
                workContext.WeekComments.Add(weekComment);
                workContext.SaveChanges();
                Flag = true;
            }
        }

        private void EditComment(UpdateWeekComment updateWeekComment)
        {
            using (var workContext = new DefaultContext())
            {
                var comment = workContext.WeekComments.FirstOrDefault(x => x.ID == updateWeekComment.ID);
                if (comment.Null())
                {
                    Message = "修改的评价不存在";
                    return;
                }
                var weekComment = updateWeekComment.UpdateComment(comment);
                workContext.ModifiedModel(weekComment);
                workContext.SaveChanges();
                Flag = true;
            }
        }

        public UpdateWeekComment GetWeekComments(Guid commentId)
        {
            var updateWeekComment = new UpdateWeekComment();
            using (var workContext = new DefaultContext())
            {
                var weekComment = workContext.WeekComments.SingleOrDefault(x => x.ID == commentId);
                if (weekComment.Null())
                {
                    Message = "修改评论不存在";
                    return null;
                }

                updateWeekComment = UpdateWeekComment.CreateUpdateWeekComment(weekComment);
            }
            return updateWeekComment;
        }

        public List<ShowWeekComent> GetShowWeekComents(Guid targetId)
        {
            var showWeekComentList = new List<ShowWeekComent>();
            using (var workContext = new DefaultContext())
            {
                var weekComments = workContext.WeekComments.Where(x => x.WeekTargetId == targetId).ToList();
                showWeekComentList.AddRange(weekComments.Select(ShowWeekComent.CreateWeekComent).ToList());
            }
            Flag = true;
            return showWeekComentList;
        }
    }
}