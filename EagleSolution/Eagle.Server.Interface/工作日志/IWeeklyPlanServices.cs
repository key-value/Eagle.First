using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IWeeklyPlanServices : IAppServices
    {
        List<ShowWeeklyPlan> Get(Guid weekPlanId, int pageNum);
        void Update(UpdateWeeklyTarget updateWeeklyTarget, Guid userId);
        void UpdateSummary(UpdateWeekSummary updateWeekSummary);
        void UpdateComment(UpdateWeekComment updateWeekComment, Guid accountId);
        UpdateWeeklyTarget Get(Guid accountId);
        UpdateWeekSummary GetWeekSummary(Guid accountId);
        UpdateWeekComment GetWeekComments(Guid commentId);
        ShowWeeklyPlan GetLastWeekPlan(Guid accountId);
        List<ShowWeekComent> GetShowWeekComents(Guid targetId);
        List<ShowWeeklyPlan> Get(int pageNum);
    }
}