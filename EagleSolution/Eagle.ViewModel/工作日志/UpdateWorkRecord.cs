using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateWorkRecord
    {

        public Guid ID
        {
            get; set;
        }

        /// <summary>
        /// 工作日志
        /// </summary>
        public string Journal
        {
            get; set;
        }

        /// <summary>
        /// 工作总结
        /// </summary>
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// 明日计划
        /// </summary>
        public string Plan
        {
            get; set;
        }

        public WorkRecord CreateWorkRecord(Guid accountId)
        {
            var workRecord = new WorkRecord();
            workRecord.ID = Guid.NewGuid();
            workRecord.AccountId = accountId;
            workRecord.CreateTime = DateTime.Now;
            workRecord.Journal = Journal;
            workRecord.Plan = Plan;
            workRecord.Summary = Summary;
            return workRecord;
        }

        public WorkRecord Update(WorkRecord workRecord)
        {
            workRecord.Journal = Journal;
            workRecord.Plan = Plan;
            workRecord.Summary = Summary;
            return workRecord;
        }

        public static UpdateWorkRecord CreateUpdateWorkRecord(WorkRecord workRecord)
        {
            var updateWorkRecord = new UpdateWorkRecord();
            updateWorkRecord.ID = workRecord.ID;
            updateWorkRecord.Journal = workRecord.Journal;
            updateWorkRecord.Summary = workRecord.Summary;
            updateWorkRecord.Plan = workRecord.Plan;
            return updateWorkRecord;
        }
    }
}