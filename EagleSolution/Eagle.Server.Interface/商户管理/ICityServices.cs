using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ICityServices : IAppServices
    {
        void Init();

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns></returns>
        List<ShowCity> GetCities();
    }
}