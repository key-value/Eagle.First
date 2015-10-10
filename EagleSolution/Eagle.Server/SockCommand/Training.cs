using System;
using System.Collections.Generic;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Server.SockCommand
{
    public class Training : BaseCommand
    {
        public Training(Guid restaurantID)
            : base(CommandType.Training, restaurantID)
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