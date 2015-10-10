using System;

namespace Eagle.ViewModel
{
    public class ShowStep
    {
        public Guid ID { get; set; }

        /// <summary>
        ///  链接吉利端时间
        /// </summary>
        public DateTime SockCreateTime { get; set; }

        /// <summary>
        /// 链接是否成功
        /// </summary>
        public int ConnectResult { get; set; }

        /// <summary>
        /// 1:登陆,2:登出,3:重新登陆,4:登陆失败，5:抢登陆失败 6:超时
        /// </summary>
        public string ConnectName { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        public int ConnectType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string IpAddress { get; set; }


        public string CreateTime { get; set; }
    }
}