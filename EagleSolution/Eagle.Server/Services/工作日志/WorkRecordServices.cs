using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IWorkRecordServices))]
    public class WorkRecordServices : ApplicationServices, IWorkRecordServices
    {
        public List<ShowWorkRecord> Get(Guid accountId, int pageNum)
        {
            var showWorkRecords = new List<ShowWorkRecord>();

            using (var workContext = new DefaultContext())
            {
                var account = workContext.Accounts.FirstOrDefault(x => x.ID == accountId);
                if (account.Null())
                {
                    return new List<ShowWorkRecord>();
                }
                var workRecords =
                    workContext.WorkRecords.AsNoTracking().Where(x => x.AccountId == accountId)
                        .OrderByDescending(x => x.CreateTime)
                        .Pageing(pageNum, PageSize, ref _pageCount);

                showWorkRecords.AddRange(workRecords.Select(x => ShowWorkRecord.CreateWorkRecord(x, account)));

            }
            return showWorkRecords;
        }

        public List<ShowWorkRecord> GetDepartment(Guid accountId, int pageNum)
        {
            var showWorkRecords = new List<ShowWorkRecord>();

            using (var workContext = new DefaultContext())
            {
                var accouCard = (
                    from card in workContext.WorkCards
                    join account in workContext.Accounts on card.AccountId equals account.ID
                    where card.Owner && card.AccountId == accountId
                    select new
                    {
                        account,
                        card
                    }).FirstOrDefault();

                if (accouCard.Null())
                {
                    return new List<ShowWorkRecord>();
                }
                var workRecords = (
                                from workRecord in workContext.WorkRecords
                                join workCard in workContext.WorkCards.Where(x => x.DepartmentId == accouCard.card.DepartmentId && x.AccountId != accountId) on workRecord.AccountId equals workCard.AccountId
                                join account1 in workContext.Accounts on workCard.AccountId equals account1.ID
                                orderby workRecord.CreateTime descending
                                select new
                                {
                                    workRecord,
                                    account1
                                }).AsNoTracking()
.Pageing(pageNum, PageSize, ref _pageCount);

                showWorkRecords.AddRange(workRecords.Select(x => ShowWorkRecord.CreateWorkRecord(x.workRecord, x.account1)));

            }
            return showWorkRecords;
        }

        public List<ShowWorkRecord> GetAllRecords(int pageNum, Guid departmentId)
        {
            var showWorkRecords = new List<ShowWorkRecord>();

            using (var workContext = new DefaultContext())
            {
                var workRecords = (
                                from workRecord in workContext.WorkRecords
                                join account1 in workContext.Accounts on workRecord.AccountId equals account1.ID
                                join workCard in workContext.WorkCards on workRecord.AccountId equals workCard.AccountId
                                where departmentId == Guid.Empty || workCard.DepartmentId == departmentId
                                orderby workRecord.CreateTime descending
                                select new
                                {
                                    workRecord,
                                    account1
                                }).AsNoTracking().Pageing(pageNum, PageSize, ref _pageCount);

                showWorkRecords.AddRange(workRecords.Select(x => ShowWorkRecord.CreateWorkRecord(x.workRecord, x.account1)));

            }
            return showWorkRecords;
        }


        public void Update(UpdateWorkRecord updateWorkRecord, Guid accountId)
        {
            if (updateWorkRecord.ID.Null())
            {
                Add(updateWorkRecord, accountId);
            }
            else
            {
                Edit(updateWorkRecord);
            }
        }

        private void Add(UpdateWorkRecord updateWorkRecord, Guid accountId)
        {
            using (var workContext = new DefaultContext())
            {
                var account = workContext.Accounts.FirstOrDefault(x => x.ID == accountId);
                if (account.Null())
                {
                    Message = "登录过期，清重新登陆";
                    return;
                }
                var workRecord = updateWorkRecord.CreateWorkRecord(account.ID);
                workContext.WorkRecords.Add(workRecord);
                workContext.SaveChanges();
                Flag = true;
            }
        }

        private void Edit(UpdateWorkRecord updateWorkRecord)
        {
            using (var workContext = new DefaultContext())
            {
                var workRecord = workContext.WorkRecords.FirstOrDefault(x => x.ID == updateWorkRecord.ID);
                workRecord = updateWorkRecord.Update(workRecord);
                workContext.ModifiedModel(workRecord);
                workContext.SaveChanges();
                Flag = true;
            }
        }

        public UpdateWorkRecord Get(Guid accountId, Guid recordId)
        {
            var updateWorkRecord = new UpdateWorkRecord();

            using (var workContext = new DefaultContext())
            {
                var workRecords =
                    workContext.WorkRecords.AsNoTracking().FirstOrDefault(x => x.AccountId == accountId && x.ID == recordId);

                updateWorkRecord = UpdateWorkRecord.CreateUpdateWorkRecord(workRecords);

            }
            return updateWorkRecord;
        }

        public List<ShowWorkComment> GetWorkComments(Guid recordId, Guid accountId)
        {
            var showWorkComments = new List<ShowWorkComment>();
            using (var workContext = new DefaultContext())
            {
                var workComments = workContext.WorkComments.AsNoTracking().Where(x => x.RecordId == recordId && (x.AccountId == accountId || x.CommentAccount == accountId)).ToList();
                showWorkComments.AddRange(workComments.Select(ShowWorkComment.CreateWorkRecord));
            }

            return showWorkComments;
        }
        public List<ShowWorkComment> GetWorkComments(Guid recordId)
        {
            var showWorkComments = new List<ShowWorkComment>();
            using (var workContext = new DefaultContext())
            {
                var workComments = workContext.WorkComments.AsNoTracking().Where(x => x.RecordId == recordId).ToList();
                showWorkComments.AddRange(workComments.Select(ShowWorkComment.CreateWorkRecord));
            }

            return showWorkComments;
        }

        public UpdateWorkComment GetWorkComment(Guid accountId, Guid commentId)
        {
            var updateWorkComment = new UpdateWorkComment();

            using (var workContext = new DefaultContext())
            {
                var workComment =
                    workContext.WorkComments.AsNoTracking().FirstOrDefault(x => x.CommentAccount == accountId && x.ID == commentId);

                updateWorkComment = UpdateWorkComment.CreateUpWorkRecord(workComment);

            }
            return updateWorkComment;
        }

        public void UpdateComment(UpdateWorkComment updateWorkComment, Guid accountId)
        {
            if (updateWorkComment.ID.Null())
            {
                AddComment(updateWorkComment, accountId);
            }
            else
            {
                EditComment(updateWorkComment);
            }
        }
        private void AddComment(UpdateWorkComment updateWorkComment, Guid accountId)
        {
            using (var workContext = new DefaultContext())
            {
                var workRecord = workContext.WorkRecords.FirstOrDefault(x => x.ID == updateWorkComment.RecordId);
                if (workRecord.Null())
                {
                    Message = "评论日志不存在";
                    return;
                }
                var workComment = updateWorkComment.Create(accountId, workRecord.AccountId);
                workContext.WorkComments.Add(workComment);
                workContext.SaveChanges();
                Flag = true;
            }
        }
        private void EditComment(UpdateWorkComment updateWorkComment)
        {
            using (var workContext = new DefaultContext())
            {
                var workComment = workContext.WorkComments.FirstOrDefault(x => x.ID == updateWorkComment.ID);
                workComment = updateWorkComment.Update(workComment);
                workContext.ModifiedModel(workComment);
                workContext.SaveChanges();
                Flag = true;
            }
        }
    }
}