using System;
using System.Runtime;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class WorkRecord : IEntity
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

        public DateTime CreateTime
        {
            get; set;
        }

        public Guid AccountId
        {
            get; set;
        }
    }
}