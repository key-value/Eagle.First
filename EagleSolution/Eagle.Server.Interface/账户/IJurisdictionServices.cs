using System;
using System.Collections.Generic;

namespace Eagle.Server.Interface
{
    public interface IJurisdictionServices : IAppServices
    {
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<Guid> GetJurisdiction(Guid accountId);

        /// <summary>
        /// 分配页面权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="addList"></param>
        /// <param name="delList"></param>
        void Distribution(Guid accountId, List<Guid> addList, List<Guid> delList);
    }
}