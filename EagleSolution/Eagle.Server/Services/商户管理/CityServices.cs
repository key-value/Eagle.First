using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.AuspiciousCache;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ICityServices))]
    public class CityServices : ApplicationServices, ICityServices
    {
        public void Init()
        {
            var monitorDbName = string.Empty;
            int result = 0;
            using (var restContext = new RestContext())
            {
                monitorDbName = restContext.Database.Connection.Database;
                string sql = string.Format("DELETE [{0}].dbo.MonitorCities", monitorDbName);
                result = restContext.Database.ExecuteSqlCommand(sql);

                sql = string.Format("INSERT INTO [{1}].dbo.MonitorCities (ID,Name) SELECT a.ID,a.Name from {0}.dbo.Cities a ", AuspiciousCache.MonitorDatabase, monitorDbName);
                result = restContext.Database.ExecuteSqlCommand(sql);
            }
            Flag = true;
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns></returns>
        public List<ShowCity> GetCities()
        {
            var cities = new List<ShowCity>();
            cities.Add(new ShowCity() { ID = Guid.Empty, Name = "全部" });
            using (var restContext = new RestContext())
            {
                var monitorCities = restContext.MonitorCities.OrderBy(x => x.Name);
                cities.AddRange(
                    from monitorCity in monitorCities
                    select new ShowCity()
                    {
                        ID = monitorCity.ID,
                        Name = monitorCity.Name,
                    });
            }

            return cities;
        }
    }
}