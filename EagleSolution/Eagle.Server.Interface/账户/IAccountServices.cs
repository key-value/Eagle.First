using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server.Interface
{
    public interface IAccountServices : IAppServices
    {
        ILoginAccount Login(string loginID, string pwd);
        List<ShowAccount> GetAccounts(int pageNum, Guid accountId);
        void Update(UpdateAccount updateAccount);
        void Delete(List<Guid> accountIdList);
        UpdateAccount GetAccount(Guid id);
        List<ShowAccount> GetAccounts(Guid accountId);
        List<ShowAccount> GetAccountsByDepartment(Guid departmentId);
        void ChangePassword(ChangeAccount changeAccount);
    }
}