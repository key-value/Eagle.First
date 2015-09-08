using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IRestaurantServices : IAppServices
    {
        List<ShowRestaurant> Get(Guid cityId, string restName, int stateType, int pageNum);
    }
}