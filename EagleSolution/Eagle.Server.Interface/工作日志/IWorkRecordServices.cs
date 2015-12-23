using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IWorkRecordServices : IAppServices
    {
        List<ShowWorkRecord> Get(Guid accountId, int pageNum);
        void Update(UpdateWorkRecord updateWorkRecord, Guid accountId);
        UpdateWorkRecord Get(Guid accountId, Guid recordId);
        List<ShowWorkComment> GetWorkComments(Guid recordId, Guid accountId);
        List<ShowWorkRecord> GetDepartment(Guid accountId, int pageNum);
        UpdateWorkComment GetWorkComment(Guid accountId, Guid commentId);
        void UpdateComment(UpdateWorkComment updateWorkComment, Guid accountId);
        List<ShowWorkRecord> GetAllRecords(DateTime selectTime, Guid departmentId);
        List<ShowWorkComment> GetWorkComments(Guid recordId);
        void Delete(UpdateWorkRecord updateWorkRecord);
    }
}