using System;
using Eagle.Infrastructrue;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateAccount
    {
        public Guid ID
        {
            get; set;
        }

        public string LoginID
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public bool State
        {
            get; set;
        }

        public Guid DepartmentId
        {
            get; set;
        }

        public Account CreateAccount()
        {
            var account = new Account();
            account.ID = Guid.NewGuid();
            account.LoginID = LoginID;
            account.Name = Name;
            account.State = true;
            account.CreateTime = DateTime.Now;
            account.LastLoginTime = DateTime.Now;
            account.SetPassword(SystemConst.DefaultPassword);
            return account;
        }

        public static UpdateAccount CreateUpdateAccount(Account account)
        {
            var updateAccount = new UpdateAccount();
            updateAccount.ID = account.ID;
            updateAccount.LoginID = account.LoginID;
            updateAccount.Name = account.Name;
            updateAccount.State = account.State;
            return updateAccount;
        }
    }
}