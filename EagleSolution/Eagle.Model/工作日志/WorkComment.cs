using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class WorkComment : IEntity
    {
        public Guid ID
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

        public Guid CommentAccount
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public Guid RecordId
        {
            get; set;
        }
    }
}