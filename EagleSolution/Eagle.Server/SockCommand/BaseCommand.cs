using System;
using System.Collections.Generic;
using System.Threading;
using Client;
using Eagle.Infrastructrue.AuspiciousCache;
using Eagle.Infrastructrue.Utility;
using Eagle.ViewModel;

namespace Eagle.Server.SockCommand
{
    public class BaseCommand
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public BaseCommand(CommandType commandType, Guid restaurantId)
        {
            CommandType = commandType;
            RestaurantId = restaurantId;
            Code = 1;
            Result = false;
        }

        public int Code { get; private set; }

        public bool Result { get; private set; }

        private string Message { get; set; }

        protected CommandType CommandType;

        public Guid RestaurantId;

        protected void PushCommandToProxy(string transferObject)
        {
            var commandLineClient = new MonitorClient(AuspiciousCache.AuspiciousIp, AuspiciousCache.AuspiciousPort);
            commandLineClient.SendDataToProxy(Guid.NewGuid(), (int)CommandType,
                string.Format("{0}:{1}", RestaurantId, transferObject));
        }

        protected void PushCommandToRest(string transferObject)
        {
            var commandLineClient = new MonitorClient(AuspiciousCache.AuspiciousIp, AuspiciousCache.AuspiciousPort);
            commandLineClient.SendDataToRest(Guid.NewGuid(), (int)CommandType, RestaurantId,
             transferObject);

            commandLineClient.ResultResponse += CommandLineClient_ResultResponse;
            int index = 0;
            while (index < 15 && Code == 1)
            {
                index++;
                Thread.Sleep(200);
            }
        }

        private void CommandLineClient_ResultResponse(bool result, int code)
        {
            Result = result;
            Code = code;
            if (Result)
            {
                Message = "发送成功";
            }
            else if (_messageDic.ContainsKey(Code))
            {
                Message = _messageDic[Code];
            }
            else
            {
                Message = "未知错误";
            }
            LogUtility.SendDebug(string.Format("接收到发送结果:{0}:{1}", result, code));
        }

        private Dictionary<int, string> _messageDic = new Dictionary<int, string>()
        {{-1,"餐厅未连接"},{-2,"餐厅连接超时"},{-3,"获取餐厅连接对象失败"}
        };

        public Cells GetResult()
        {
            return new Cells(Result, Message, Code);
        }
    }
}
