using System;
using System.Collections.Generic;

namespace Eagle.Model
{
    public class MonitorConnectStep
    {
        public Guid ID { get; set; }

        public Guid RestaurantID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  链接吉利端时间
        /// </summary>
        public DateTime SockCreateTime { get; set; }

        /// <summary>
        /// 吉利端发送时间
        /// </summary>
        public DateTime SockSendTime { get; set; }

        /// <summary>
        /// 链接是否成功
        /// </summary>
        public int ConnectResult { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        public int ConnectType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string IpAddress { get; set; }



        public static List<string> ConnectName = new List<string>()
        {
            "",
            "登陆",
            "登出",
            "重新登陆",
            "登陆失败",
            "抢登陆失败",
            "超时",
        };
    }
}