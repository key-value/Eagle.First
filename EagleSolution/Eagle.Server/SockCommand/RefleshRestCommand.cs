using System;
using System.Collections.Generic;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Server.SockCommand
{
    public class RefleshRestCommand : BaseCommand
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public RefleshRestCommand()
            : base(CommandType.RefleshRest, Guid.Empty)
        {

        }


        public void Work()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //dictionary.Add("CommandType", ((int)CommandType).ToString());
            PushCommandToProxy(dictionary.ToJson());
        }
    }
}
