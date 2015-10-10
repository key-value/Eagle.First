using System;
using System.Collections.Generic;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Server.SockCommand
{
    public class UpLoadLog : BaseCommand
    {
        public UpLoadLog(Guid restaurantID)
            : base(CommandType.UpLoadLog, restaurantID)
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