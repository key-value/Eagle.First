using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowWorkComment
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

        public static ShowWorkComment CreateWorkRecord(WorkComment workComment)
        {
            var showWorkComment = new ShowWorkComment();
            showWorkComment.ID = workComment.ID;
            showWorkComment.CreateTime = workComment.CreateTime;
            showWorkComment.AccountId = workComment.AccountId;
            showWorkComment.Description = workComment.Description;

            return showWorkComment;
        }
    }
}