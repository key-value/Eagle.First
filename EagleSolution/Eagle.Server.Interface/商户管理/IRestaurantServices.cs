using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IRestaurantServices : IAppServices
    {
        List<ShowRestaurant> Get(Guid cityId, string restName, int stateType, int pageNum);

        /// <summary>
        /// 0为非对接  1为对接
        /// </summary>
        /// <param name="dockMode"></param>
        /// <returns></returns>
        List<IShowRestName> Get(string dockMode);

        List<ShowRestaurant> GetAll(Guid cityId, string restName, int pageNum);
    }
}