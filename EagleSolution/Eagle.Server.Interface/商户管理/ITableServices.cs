using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ITableServices : IAppServices
    {
        /// <summary>
        /// 导入餐桌数据
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// 获取餐桌列表
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        List<ShowTable> GetDeskList(Guid restaurantId, int pageNum);
    }
}