using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    /// <summary>
    /// 员工工牌
    /// </summary>
    public class WorkCard : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }

        public Guid AccountId
        {
            get; set;
        }


        public bool Owner
        {
            get; set;
        }
    }
}