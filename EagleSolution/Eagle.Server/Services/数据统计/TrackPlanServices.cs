using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ITrackPlanServices))]
    public class TrackPlanServices : ApplicationServices, ITrackPlanServices
    {
        public List<ShowTrackPlan> Get(int pageNum)
        {
            var showTrackPlans = new List<ShowTrackPlan>();
            using (var defaultContext = new DefaultContext())
            {
                var trackPlans = defaultContext.TrackPlans.OrderByDescending(x => x.CreateTime).AsNoTracking()
                    .Pageing(pageNum, PageSize, ref _pageCount);

                showTrackPlans.AddRange(trackPlans.Select(ShowTrackPlan.CreateShowTrackPlan));
            }
            return showTrackPlans;
        }
        public List<ISelectTrackPlan> Get()
        {
            var showTrackPlans = new List<ISelectTrackPlan>();
            using (var defaultContext = new DefaultContext())
            {
                //.Where(x => x.BeginTime < DateTime.Now && x.EndTime > DateTime.Now)
                var trackPlans = defaultContext.TrackPlans.OrderByDescending(x => x.CreateTime).AsNoTracking();

                showTrackPlans.AddRange(trackPlans.Select(ShowTrackPlan.CreateShowTrackPlan));
            }
            return showTrackPlans;
        }
        public UpdateTrackPlan Get(Guid trackPlanId)
        {
            using (var defaultContext = new DefaultContext())
            {
                var trackPlan = defaultContext.TrackPlans.SingleOrDefault(x => x.ID == trackPlanId);

                return UpdateTrackPlan.CreateUpdateTrackPlan(trackPlan);
            }
        }

        public void Update(UpdateTrackPlan updateTrackPlan)
        {
            if (updateTrackPlan.ID.Null())
            {
                Add(updateTrackPlan);
            }
            else
            {
                Edit(updateTrackPlan);
            }
        }
        private void Add(UpdateTrackPlan updateTrackPlan)
        {
            using (var defaultContext = new DefaultContext())
            {
                defaultContext.TrackPlans.Add(updateTrackPlan.Create());
                defaultContext.SaveChanges();
            }

            Flag = true;
        }
        private void Edit(UpdateTrackPlan updateTrackPlan)
        {
            using (var defaultContext = new DefaultContext())
            {
                var trackPlan = defaultContext.TrackPlans.SingleOrDefault(x => x.ID == updateTrackPlan.ID);
                if (trackPlan.Null())
                {
                    return;
                }
                defaultContext.TrackPlans.Add(updateTrackPlan.Create());
                defaultContext.SaveChanges();
            }
            Flag = true;
        }

        public void Delete(List<Guid> trackIdList)
        {
            using (var defalutContent = new DefaultContext())
            {
                var trackPlans = defalutContent.TrackPlans.Where(x => trackIdList.Contains(x.ID));
                if (!trackPlans.Any())
                {
                    Message = "请选择要删除的数据";
                    return;
                }
                defalutContent.TrackPlans.RemoveRange(trackPlans);
                var trackTargets = defalutContent.TrackTargets.Where(x => trackIdList.Contains(x.PlanId));
                if (trackTargets.Any())
                {
                    defalutContent.TrackTargets.RemoveRange(trackTargets);
                }
                var trackRecords = defalutContent.TrackRecords.Where(x => trackIdList.Contains(x.PlanId));
                if (trackTargets.Any())
                {
                    defalutContent.TrackRecords.RemoveRange(trackRecords);
                }

                defalutContent.SaveChanges();
                Flag = true;
            }
        }

        public void Vote(Guid planId, Guid targetId, string userKey)
        {
            using (var defalutContent = new DefaultContext())
            {
                var plan = defalutContent.TrackPlans.SingleOrDefault(x => x.ID == planId);
                if (plan.Null() || plan.BeginTime > DateTime.Now || plan.EndTime < DateTime.Now)
                {
                    return;
                }
                var trackRecord = new TrackRecord();
                trackRecord.ID = Guid.NewGuid();
                trackRecord.AllKey = userKey;
                trackRecord.CreateTime = DateTime.Now;
                trackRecord.PlanId = planId;
                trackRecord.TargetId = targetId;
                Flag = true;
            }
        }
    }
}