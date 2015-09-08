using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server.Interface
{
    public interface IAccountServices : IAppServices
    {
        ILoginAccount Login(string loginID, string pwd);
        List<ShowAccount> GetAccounts(int pageNum);
        void Update(UpdateAccount updateAccount);
        void Delete(List<Guid> accountIdList);
        UpdateAccount GetAccount(Guid id);
    }
}