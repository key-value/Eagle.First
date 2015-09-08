using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class RestaurantState : IAggregateRoot
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 连接状态 0为未连接
        /// </summary>
        public bool ConnectState { get; set; }

        /// <summary>
        /// 最后链接时间
        /// </summary>
        public DateTime LastConnectTime { get; set; }

        /// <summary>
        /// 餐厅Ip地址
        /// </summary>
        public string IpAddress { get; set; }
    }
}