using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Eagle.Domain.EF;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IRestaurantServices))]
    public class RestaurantServices : ApplicationServices, IRestaurantServices
    {
        public List<ShowRestaurant> Get(Guid cityId, string restName, int stateType, int pageNum)
        {
            Expression<Func<MonitorRestaurant, bool>> restaurantExpression = x => true;
            Expression<Func<MonitorRestaurant, bool>> restaurantNameExpression = x => true;
            if (Guid.Empty != cityId)
            {
                restaurantExpression = x => x.City == cityId;
            }
            Guid restaurant = new Guid();
            if (!string.IsNullOrEmpty(restName))
            {
                if (Guid.TryParse(restName, out restaurant))
                {
                    restaurantNameExpression = x => x.ID == restaurant;
                }
                else
                {
                    restaurantNameExpression = x => x.Name.Contains(restName) || x.SubName.Contains(restName);
                }
            }
            Expression<Func<Model.RestaurantState, bool>> stateExpression = x => true;
            if (stateType != 0)
            {
                var connectState = stateType != 2;
                stateExpression = state => state.ConnectState == connectState;
            }
            var showRests = new List<ShowRestaurant>();
            using (var restContext = new RestContext())
            {
                var restLists =
                    (from monitorRestaurant in restContext.MonitorRestaurants
                        .Where(restaurantExpression).Where(restaurantNameExpression)
                     join state in restContext.RestaurantStates.Where(stateExpression) on monitorRestaurant.ID equals state.ID
                     orderby state.LastConnectTime descending
                     orderby state.ConnectState descending
                     orderby monitorRestaurant.SubName
                     orderby monitorRestaurant.Name
                     select new { monitorRestaurant, state }).AsNoTracking().ToList();

                //var restLists =
                //    restContext.MonitorRestaurants.AsNoTracking()
                //        .Where(restaurantExpression).Where(restaurantNameExpression)
                //        .OrderBy(x => x.Name)
                //        .ThenBy(x => x.SubName).Pageing(pageNum, PageSize, ref _pageCount)
                //        .ToList();
                showRests.AddRange(restLists.Select(x => ShowRestaurant.CreateShowRestaurant(x.monitorRestaurant, x.state)));

            }
            return showRests;
        }
    }
}