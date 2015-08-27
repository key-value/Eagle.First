using System;
using System.Data;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowAccount
    {
        public Guid ID { get; set; }

        public string LoginID { get; set; }

        public string Name { get; set; }

        public string LastLoginTime { get; set; }

        public bool State { get; set; }

        public string CreateTime { get; set; }

        public static ShowAccount CreateShowAccount(Account account)
        {
            var showAccount = new ShowAccount();
            showAccount.ID = account.ID;
            showAccount.LoginID = account.LoginID;
            showAccount.Name = account.Name;
            showAccount.LastLoginTime = account.LastLoginTime.ToString("yy-MM-dd");
            showAccount.State = account.State;
            showAccount.CreateTime = account.CreateTime.ToString("yy-MM-dd hh:mm:ss");
            return showAccount;
        }
    }
}