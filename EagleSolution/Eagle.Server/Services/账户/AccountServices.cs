using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server.Interface;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IAccountServices))]
    public class AccountServices : ApplicationServices, IAccountServices
    {
        public ILoginAccount Login(string loginID, string pwd)
        {
            using (var defaultContext = new DefaultContext())
            {
                var a = defaultContext.SystemCards.FirstOrDefault();
            }
            using (var accountContext = new DefaultContext())
            {
                var account = accountContext.Accounts.FirstOrDefault(x => x.LoginID == loginID.Trim());
                if (account == null)
                {
                    Message = "输入账号不存在";
                    return null;
                }
                if (!account.State)
                {
                    Message = "账号已禁用";
                    return null;
                }
                if (account.Password != pwd.MD5().ToLower())
                {
                    Message = "密码错误请重新输入";
                    return null;
                }
                Flag = true;
                return ShowAccount.CreateShowAccount(account);
            }
        }


        public List<ShowAccount> GetAccounts(int pageNum)
        {
            var accounts = new List<ShowAccount>();
            using (var defaultContent = new DefaultContext())
            {
                var accountList = defaultContent.Accounts.OrderByDescending(x => x.CreateTime)
                    .Pageing(pageNum, PageSize, ref _pageCount).ToList();

                accounts.AddRange(accountList.Select(ShowAccount.CreateShowAccount));
            }
            return accounts;
        }

        public UpdateAccount GetAccount(Guid id)
        {
            using (var defaultContent = new DefaultContext())
            {
                var account = defaultContent.Accounts.FirstOrDefault(x => x.ID == id);
                if (account.Null())
                {
                    Message = "未找到要修改的账号";
                    return null;
                }
                var updateAccount = ViewModel.UpdateAccount.CreateUpdateAccount(account);
                Flag = true;
                return updateAccount;
            }
        }

        public void Add(UpdateAccount updateAccount)
        {
            using (var defaultContent = new DefaultContext())
            {
                var exit = defaultContent.Accounts.Any(x => x.LoginID == updateAccount.LoginID.Trim());
                if (exit)
                {
                    Flag = false;
                    Message = "已经存在相同的账号";
                    return;
                }
                var account = updateAccount.CreateAccount();
                defaultContent.Accounts.Add(account);
                defaultContent.SaveChanges();
                Flag = true;
            }
        }

        public void Edit(UpdateAccount updateAccount)
        {
            using (var defaultContent = new DefaultContext())
            {
                var account = defaultContent.Accounts.FirstOrDefault(x => x.ID == updateAccount.ID);
                if (account.Null())
                {
                    Message = "请选择要修改的账号";
                    return;
                }
                account.LastLoginTime = DateTime.Now;
                account.LoginID = updateAccount.LoginID;
                account.Name = updateAccount.Name;
                defaultContent.ModifiedModel(account);
                defaultContent.SaveChanges();
            }
            Flag = true;
        }

        public void Update(UpdateAccount updateAccount)
        {
            if (updateAccount.ID == Guid.Empty)
            {
                Add(updateAccount);
            }
            else
            {
                Edit(updateAccount);
            }
        }

        public void Delete(List<Guid> accountIdList)
        {
            using (var defalutContent = new DefaultContext())
            {
                var accounts = defalutContent.Accounts.Where(x => accountIdList.Contains(x.ID));
                if (!accounts.Any())
                {
                    Message = "请选择要删除的数据";
                    return;
                }
                defalutContent.Accounts.RemoveRange(accounts);
                defalutContent.SaveChanges();
                Flag = true;
            }
        }
    }
}