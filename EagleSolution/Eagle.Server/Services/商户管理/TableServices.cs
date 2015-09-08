using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.AuspiciousCache;
using Eagle.Infrastructrue.Utility;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ITableServices))]
    public class TableServices : ApplicationServices, ITableServices
    {

        /// <summary>
        /// 导入餐桌数据
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            var monitorDbName = string.Empty;
            int result = 0;
            using (var restContext = new RestContext())
            {
                monitorDbName = restContext.Database.Connection.Database;
                string sql = string.Format("DELETE [{0}].[dbo].[MonitorTables]", monitorDbName);
                result = restContext.Database.ExecuteSqlCommand(sql);

                sql = String.Format("INSERT INTO [{1}].[dbo].[MonitorTables] (ID,RestaurantID,TableName,IsDelete,TableType,UpdateTime) SELECT ID,RestaurantID,Name,[Delete],TableType,GETDATE() FROM [{0}].dbo.Tables", AuspiciousCache.MonitorDatabase, monitorDbName);
                result = restContext.Database.ExecuteSqlCommand(sql);
            }
            Flag = true;
            return result;
        }


        /// <summary>
        /// 获取餐桌列表
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public List<ShowTable> GetDeskList(Guid restaurantId, int pageNum)
        {
            var desks = new List<ShowTable>();
            using (var restContext = new RestContext())
            {
                var monitorDesks = restContext.MonitorTables.Where(x => x.RestaurantId == restaurantId).OrderBy(x => x.IsDelete).ThenBy(x => x.TableName).Pageing(pageNum, PageSize, ref _pageCount).ToList();
                desks.AddRange(monitorDesks.Select(x => new ShowTable()
                {
                    ID = x.ID,
                    TableName = x.TableName,
                    TableType = x.TableType,
                    IsDelete = x.IsDelete,
                    UpdateTime = x.UpdateTime,
                }));
            }
            return desks;
        }


    }
}