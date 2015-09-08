using System;

namespace Eagle.Model
{
    public class MonitorRestaurant
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
        /// 分店名
        /// </summary>
        public string SubName { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        public Guid City { get; set; }

        /// <summary>
        /// 是否开通体验店
        /// </summary>
        public bool IsExperience { get; set; }

        /// <summary>
        /// 餐厅代理版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 餐厅Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 餐饮系统
        /// </summary>
        public string CateringSystem { get; set; }

        /// <summary>
        /// 餐饮系统
        /// </summary>
        public string DockMode { get; set; }

        public string GetShowName()
        {
            if (string.IsNullOrEmpty(SubName))
            {
                return Name;
            }
            return string.Format("{0}({1})", Name, SubName);
        }
    }
}