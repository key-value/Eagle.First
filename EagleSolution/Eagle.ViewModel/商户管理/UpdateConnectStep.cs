using System;

using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateConnectStep
    {
        public Guid ID { get; set; }

        public Guid RestaurantId { get; set; }

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

        public MonitorConnectStep CreateConnectStep()
        {
            var connectStep = new MonitorConnectStep();
            connectStep.ID = ID;
            connectStep.RestaurantID = RestaurantId;
            connectStep.SockCreateTime = SockCreateTime;
            connectStep.CreateTime = DateTime.Now;
            connectStep.IpAddress = IpAddress;
            connectStep.ConnectType = ConnectType;
            connectStep.ConnectResult = 1;
            connectStep.SockCreateTime = SockCreateTime;
            connectStep.SockSendTime = SockSendTime;
            return connectStep;

        }
    }
}