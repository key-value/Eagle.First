using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.AuspiciousCache;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IMonitorConnectStepServices))]
    public class MonitorConnectStepServices : ApplicationServices, IMonitorConnectStepServices
    {
        public List<ShowStep> GetMonitorConnectSteps(Guid restaurantId, int pageNum)
        {
            Expression<Func<MonitorConnectStep, bool>> auspiciousExpression = x => x.RestaurantID == restaurantId;
            var monitorConnectSteps = new List<ShowStep>();

            using (var monitorContext = new DefaultContext())
            {
                var connectSteps =
                    monitorContext.MonitorConnectSteps
                        .Where(auspiciousExpression)
                        .OrderByDescending(x => x.CreateTime)
                        .Pageing(pageNum, PageSize, ref _pageCount);
                monitorConnectSteps.AddRange(from monitorConnectStep in connectSteps
                                             select new ShowStep()
                                             {
                                                 ID = monitorConnectStep.ID,
                                                 SockCreateTime = monitorConnectStep.SockCreateTime,
                                                 ConnectResult = monitorConnectStep.ConnectResult,
                                                 ConnectType = monitorConnectStep.ConnectType,
                                                 IpAddress = monitorConnectStep.IpAddress,
                                                 ConnectName = MonitorConnectStep.ConnectName[monitorConnectStep.ConnectType],
                                                 CreateTime = monitorConnectStep.SockCreateTime.ToString("h:mm tt"),
                                             }
                );
            }
            if (monitorConnectSteps.Any())
            {
                monitorConnectSteps.Add(new ShowStep()
                {
                    ConnectType = 0,
                    ConnectResult = 0,
                });
            }
            return monitorConnectSteps;
        }

        public void UpdateConnectStep(UpdateConnectStep updateConnectStep)
        {
            var connnectStep = updateConnectStep.CreateConnectStep();

            using (var restContext = new DefaultContext())
            {
                var restaurant = restContext.MonitorRestaurants.SingleOrDefault(x => x.ID == connnectStep.RestaurantID);
                restContext.MonitorConnectSteps.Add(connnectStep);
                if (restaurant.Null())
                {
                    var results =
                        restContext.Database.SqlQuery<MonitorRestaurant>(
                            string.Format(
                                "SELECT a.ID,b.Name,b.SubName,b.CityID City,a.Smart IsExperience,convert(nvarchar,coalesce(a.FirstVersion,'1'))+'.'+convert(nvarchar,coalesce(a.SecondVersion,'0'))+'.'+convert(nvarchar,coalesce(a.ThirdVersion,'0'))+'.'+convert(nvarchar,coalesce(a.FourthVersion,'0')) Version,NULL IpAddress,CateringSystemName CateringSystem,DockMode from [{0}].dbo.[RestSetUps] a LEFT JOIN [{0}].dbo.Restaurants b on a.ID = b.ID where a.ID = '{1}'",
                                AuspiciousCache.MonitorDatabase, updateConnectStep.RestaurantId));
                    var monitorRest = results.SingleOrDefault();
                    if (monitorRest.Null())
                    {
                        connnectStep.ConnectResult = 0;
                        restContext.SaveChanges();
                        return;

                    }
                }
                var restaurantState = restContext.RestaurantStates.SingleOrDefault(x => x.ID == connnectStep.RestaurantID);
                if (restaurantState == null)
                {
                    restaurantState = new RestaurantState();
                    restaurantState.ID = connnectStep.RestaurantID;
                    restContext.RestaurantStates.Add(restaurantState);
                }
                else
                {
                    restContext.ModifiedModel(restaurantState);
                }
                restaurantState.LastConnectTime = DateTime.Now;
                bool connectState = (updateConnectStep.ConnectType == 1 || updateConnectStep.ConnectType == 3);
                if (connectState)
                {
                    restaurantState.IpAddress = connnectStep.IpAddress;
                }
                //1:登陆,2:登出,3:重新登陆,4:登陆失败，5:抢登陆失败 6:超时
                restaurantState.ConnectState = connectState;

                restContext.SaveChanges();
                Flag = true;
            }
        }

        public void RefreshRestaurantState(List<Guid> restIds)
        {
            using (var restContext = new DefaultContext())
            {
                restContext.Database.ExecuteSqlCommand("UPDATE RestaurantStates SET ConnectState = 0 ");
                foreach (var guid in restIds)
                {
                    var guid1 = guid;
                    var restaurantState = restContext.RestaurantStates.SingleOrDefault(x => x.ID == guid1);
                    if (restaurantState == null)
                    {
                        restaurantState = new RestaurantState();
                        restaurantState.ID = guid;
                        restContext.RestaurantStates.Add(restaurantState);
                    }
                    restaurantState.ConnectState = true;
                    restaurantState.LastConnectTime = DateTime.Now;
                    restContext.SaveChanges();
                }
                Flag = true;
                return;
            }
        }
    }
}