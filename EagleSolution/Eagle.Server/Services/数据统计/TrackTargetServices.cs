using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ITrackTargetServices))]
    public class TrackTargetServices : ApplicationServices, ITrackTargetServices
    {
        public List<ShowTrackTarget> Get(int pageNum)
        {
            var showTrackPlans = new List<ShowTrackTarget>();
            using (var defaultContext = new DefaultContext())
            {
                var trackTargets = defaultContext.TrackTargets.OrderByDescending(x => x.PlanId).ThenByDescending(x => x.CreateTime).AsNoTracking()
                    .Pageing(pageNum, PageSize, ref _pageCount);
                var trackPlanIds = trackTargets.Select(x => x.PlanId).Distinct().ToList();
                var trackPlan = defaultContext.TrackPlans.Where(x => trackPlanIds.Contains(x.ID));


                showTrackPlans.AddRange(trackTargets.Select(x => ShowTrackTarget.CreateShowTrackTarget(x, trackPlan.ToList())));
            }
            return showTrackPlans;
        }

        public UpdateTrackTarget Get(Guid trackTargetId)
        {
            using (var defaultContext = new DefaultContext())
            {
                var trackTarget = defaultContext.TrackTargets.SingleOrDefault(x => x.ID == trackTargetId);

                return UpdateTrackTarget.CreateUpdateTrackTarget(trackTarget);
            }
        }

        public void Update(UpdateTrackTarget updateTrackTarget)
        {
            if (updateTrackTarget.ID.Null())
            {
                Add(updateTrackTarget);
            }
            else
            {
                Edit(updateTrackTarget);
            }
        }
        private void Add(UpdateTrackTarget updateTrackTarget)
        {
            using (var defaultContext = new DefaultContext())
            {
                defaultContext.TrackTargets.Add(updateTrackTarget.Create());
                defaultContext.SaveChanges();
            }

            Flag = true;
        }
        private void Edit(UpdateTrackTarget updateTrackPlan)
        {
            using (var defaultContext = new DefaultContext())
            {
                var trackPlan = defaultContext.TrackPlans.SingleOrDefault(x => x.ID == updateTrackPlan.ID);
                if (trackPlan.Null())
                {
                    return;
                }
                defaultContext.TrackTargets.Add(updateTrackPlan.Create());
                defaultContext.SaveChanges();
            }
            Flag = true;
        }

        public void Delete(List<Guid> trackIdList)
        {
            using (var defalutContent = new DefaultContext())
            {
                var trackTargets = defalutContent.TrackTargets.Where(x => trackIdList.Contains(x.ID));
                if (trackTargets.Any())
                {
                    defalutContent.TrackTargets.RemoveRange(trackTargets);
                }
                var trackRecords = defalutContent.TrackRecords.Where(x => trackIdList.Contains(x.TargetId));
                if (trackTargets.Any())
                {
                    defalutContent.TrackRecords.RemoveRange(trackRecords);
                }

                defalutContent.SaveChanges();
                Flag = true;
            }
        }

        public void Vote(string userKey, Guid targetId)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            VoteTarget(userKey, targetId);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task VoteTarget(string userKey, Guid targetId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            using (var defaultContext = new DefaultContext())
            {
                var target = defaultContext.TrackTargets.FirstOrDefault(x => x.ID == targetId);
                if (target.Null())
                {
                    LogUtility.SendError("错误投票!!");
                }
                var trackPlan = defaultContext.TrackPlans.FirstOrDefault(x => x.ID == target.PlanId);
                if (trackPlan.Null() || trackPlan.BeginTime > DateTime.Now || trackPlan.EndTime < DateTime.Now)
                {
                    LogUtility.SendError("投票活动失败!!");
                }
                var trackRecord = new TrackRecord();
                trackRecord.ID = Guid.NewGuid();
                trackRecord.AllKey = userKey;
                trackRecord.CreateTime = DateTime.Now;
                trackRecord.PlanId = target.PlanId;
                trackRecord.TargetId = targetId;
                defaultContext.TrackRecords.Add(trackRecord);
                defaultContext.SaveChanges();
            }
        }
    }
}

