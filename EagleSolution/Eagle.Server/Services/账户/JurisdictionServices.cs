using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server.Interface;

namespace Eagle.Server.Services
{
    [Injection(typeof(IJurisdictionServices))]
    public class JurisdictionServices : ApplicationServices, IJurisdictionServices
    {
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<Guid> GetJurisdiction(Guid accountId)
        {
            Flag = true;
            using (var defaultContext = new AccountContext())
            {
                var jurisdictions = defaultContext.Jurisdictions.Where(x => x.AccountID == accountId).AsNoTracking().ToList();
                return jurisdictions.Select(x => x.BranchId).ToList();
            }
        }

        /// <summary>
        /// 分配页面权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="addList"></param>
        /// <param name="delList"></param>
        public void Distribution(Guid accountId, List<Guid> addList, List<Guid> delList)
        {
            using (var defaultContent = new AccountContext())
            {
                var account = defaultContent.Accounts.FirstOrDefault(x => x.ID == accountId);
                if (account.Null())
                {
                    Message = "分配账号不存在";
                    return;
                }
                if (delList.Any())
                {
                    var delBranch = defaultContent.Jurisdictions.Where(x => delList.Contains(x.BranchId) && x.AccountID == accountId).ToList();
                    defaultContent.Jurisdictions.RemoveRange(delBranch);
                }
                if (addList.Any())
                {
                    foreach (var guid in addList)
                    {
                        var jurisdiction = new Jurisdiction();
                        jurisdiction.ID = Guid.NewGuid();
                        jurisdiction.AccountID = accountId;
                        jurisdiction.BranchId = guid;
                        defaultContent.Jurisdictions.Add(jurisdiction);
                    }
                }
                defaultContent.SaveChanges();
                Flag = true;
            }
        }
    }
}