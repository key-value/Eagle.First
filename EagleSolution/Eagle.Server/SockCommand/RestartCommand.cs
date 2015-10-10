using System;
using System.Collections.Generic;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Server.SockCommand
{
    public class RestartCommand : BaseCommand
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public RestartCommand(Guid restaruantId)
            : base(CommandType.Restart, restaruantId)
        {

        }


        public void Work()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //dictionary.Add("CommandType", ((int)CommandType).ToString());
            PushCommandToRest(dictionary.ToJson());
        }
    }
}
