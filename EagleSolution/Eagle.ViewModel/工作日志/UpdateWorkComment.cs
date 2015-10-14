using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateWorkComment
    {
        public Guid ID
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

        public static UpdateWorkComment CreateUpWorkRecord(WorkComment workComment)
        {
            var updateWorkComment = new UpdateWorkComment();
            updateWorkComment.ID = workComment.ID;
            updateWorkComment.Description = workComment.Description;
            return updateWorkComment;
        }

        public WorkComment Update(WorkComment workComment)
        {
            workComment.Description = Description;
            return workComment;
        }

        public WorkComment Create(Guid accountId, Guid recordAccountId)
        {
            WorkComment workComment = new WorkComment();
            workComment.ID = Guid.NewGuid();
            workComment.CommentAccount = accountId;
            workComment.AccountId = recordAccountId;
            workComment.CreateTime = DateTime.Now;
            workComment.Description = Description;
            workComment.RecordId = RecordId;
            return workComment;
        }
    }
}