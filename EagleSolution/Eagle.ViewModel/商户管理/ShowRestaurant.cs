using System;
using Eagle.Infrastructrue;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowRestaurant
    {

        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 餐厅名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 餐厅代理版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 餐饮系统
        /// </summary>
        public string CateringSystem { get; set; }

        /// <summary>
        /// 餐饮系统
        /// </summary>
        public string DockMode { get; set; }

        /// <summary>
        /// 连接状态 0为未连接
        /// </summary>
        public bool ConnectState { get; set; }

        /// <summary>
        /// 最后链接时间
        /// </summary>
        public DateTime LastConnectTime { get; set; }

        public static ShowRestaurant CreateShowRestaurant(MonitorRestaurant monitorRestaurant, RestaurantState restaurantState)
        {
            var showRest = new ShowRestaurant();
            showRest.ID = monitorRestaurant.ID;
            showRest.Name = monitorRestaurant.GetShowName();
            showRest.Version = monitorRestaurant.Version;
            showRest.CateringSystem = SystemConst.GetCateringSystem(monitorRestaurant.CateringSystem);
            showRest.DockMode = monitorRestaurant.DockMode;
            return showRest;
        }

    }
}