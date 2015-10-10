using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Eagle.Domain.EF;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;

namespace Eagle.Server.SockCommand
{
    public class SendSqlCommand : BaseCommand
    {
        public SendSqlCommand(Guid restaurantID)
            : base(CommandType.SendSqlCommand, restaurantID)
        {
        }

        public void Work(string sqlText)
        {
            var restPace = new RestPace();
            restPace.ID = Guid.NewGuid();
            restPace.SqlCommand = sqlText;
            restPace.SendTime = DateTime.Now;
            restPace.ReceiveTime = SqlDateTime.MinValue.Value;
            restPace.RestaurantId = RestaurantId;
            restPace.ReceiptText = new List<List<string>>().ToJson();
            using (var restContext = new DefaultContext())
            {
                restContext.RestPaces.Add(restPace);
                restContext.SaveChanges();
            }
            var dic = new Dictionary<string, string>();
            dic.Add("messageID", restPace.ID.ToString());
            dic.Add("text", sqlText);
            PushCommandToRest(dic.ToJson());
        }
    }
}