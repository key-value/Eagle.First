using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowWorkRecord
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

        public string CreateTime
        {
            get; set;
        }

        public Guid AccountId
        {
            get; set;
        }

        public string AccountName
        {
            get; set;
        }

        public static ShowWorkRecord CreateWorkRecord(WorkRecord workRecord, Account account)
        {
            var showWorkRecord = new ShowWorkRecord();
            showWorkRecord.ID = workRecord.ID;
            showWorkRecord.CreateTime = workRecord.CreateTime.ToString("yy-MM-dd HH:mm");
            showWorkRecord.Journal = workRecord.Journal;
            showWorkRecord.Plan = workRecord.Plan;
            showWorkRecord.Summary = workRecord.Summary;
            showWorkRecord.AccountId = account.ID;
            showWorkRecord.AccountName = account.Name;
            return showWorkRecord;
        }
    }
}