using System;
using System.Data.SqlTypes;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.AuspiciousCache;
using Eagle.Model;

namespace Eagle.Server.Services
{
    [Injection(typeof(IInitMonitorRestaruant))]
    public class InitMonitorRestaruant : ApplicationServices, IInitMonitorRestaruant
    {
        /// <summary>
        /// 导入餐厅数据
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            var monitorDbName = string.Empty;
            int result = 0;
            using (var defaultContext = new DefaultContext())
            {

                monitorDbName = defaultContext.Database.Connection.Database;

                string sql = string.Format("DELETE [{0}].dbo.MonitorRestaurants", monitorDbName);

                result = defaultContext.Database.ExecuteSqlCommand(sql);
                string insertSql = string.Format("INSERT INTO [{1}].dbo.MonitorRestaurants (ID,Name,SubName,City,IsExperience,[Version],IpAddress,CateringSystem,DockMode) SELECT a.ID,b.Name,b.SubName,b.CityID,a.Smart,convert(nvarchar,coalesce(a.FirstVersion,'1'))+'.'+convert(nvarchar,coalesce(a.SecondVersion,'0'))+'.'+convert(nvarchar,coalesce(a.ThirdVersion,'0'))+'.'+convert(nvarchar,coalesce(a.FourthVersion,'0')),NULL,CateringSystemName,DockMode from {0}.dbo.[RestSetUps] a LEFT JOIN {0}.dbo.Restaurants b on a.ID = b.ID ", AuspiciousCache.MonitorDatabase, monitorDbName);
                result = defaultContext.Database.ExecuteSqlCommand(insertSql);
                Flag = true;
            }
            return result;
        }
        public int Clear()
        {
            int result = 0;
            using (var defaultContext = new DefaultContext())
            {
                var monitorDbName = defaultContext.Database.Connection.Database;
                string sql = string.Format("DELETE [{0}].dbo.MonitorRestaurants", monitorDbName);
                result = defaultContext.Database.ExecuteSqlCommand(sql);

            }
            Flag = true;
            return result;
        }
    }
}
