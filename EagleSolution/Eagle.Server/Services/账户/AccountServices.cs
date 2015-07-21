using System;
using System.Linq;
using System.Security.Principal;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;

namespace Eagle.Server.Services
{
    [Injection(typeof(IAccountServices))]
    public class AccountServices : ApplicationServices, IAccountServices
    {
        public Guid Login(string loginID, string pwd)
        {
            using (var defaultContext = new DefaultContext())
            {
                var account = defaultContext.Accounts.FirstOrDefault(x => x.LoginID == loginID.Trim());
                if (account == null)
                {
                    Message = "输入账号不存在";
                    return Guid.Empty;
                }
                if (!account.State)
                {
                    Message = "账号已禁用";
                    return Guid.Empty;
                }
                if (account.Password != pwd.MD5().ToLower())
                {
                    Message = "密码错误请重新输入";
                    return Guid.Empty;
                }
                Flag = true;
                return account.ID;
            }
        }


    }
}