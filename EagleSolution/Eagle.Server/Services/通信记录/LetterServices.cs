using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.ServiceModel;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.Interface.Wcf;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(ILetterServices))]
    public class LetterServices : ApplicationServices, ILetterServices
    {
        public void SendLetter(UpdateLetter updateLetter)
        {
            var letter = updateLetter.CreateLetter();
            letter.ReplyTime = SqlDateTime.MinValue.Value;
            using (var content = new DefaultContext())
            {
                content.Letters.Add(letter);
                content.SaveChanges();
            }
            var dic = new Dictionary<string, string>();
            dic.Add("ActionName", "Reply");
            dic.Add("letter", letter.ToJson());

            var resString = HttpWebResponseUtility.CreatePostHttpResponse("http://second.eagle.com/api/Message", dic.ToJson(), 30000);
            if (string.IsNullOrEmpty(resString) || resString == "0")
            {
                Flag = false;
                Message = "消息无响应";
                return;
            }
            var result = resString.ToDeserialize<Cells>();
            if (!result.Flag)
            {
                Flag = false;
                Message = result.Message;
                return;
            }
            Flag = true;
        }

        public List<ShowLetter> GetLetters(int pageNum)
        {
            Flag = true;
            List<ShowLetter> showLetters = new List<ShowLetter>();
            using (var content = new DefaultContext())
            {
                var dataLetters =
                    content.Letters.AsNoTracking()
                        .OrderByDescending(x => x.CreateTime)
                        .Pageing(pageNum, PageSize, ref _pageCount)
                        .ToList();
                if (dataLetters.Null())
                {
                    return new List<ShowLetter>();
                }
                foreach (var dataLetter in dataLetters)
                {
                    showLetters.Add(ShowLetter.CreateShowLetter(dataLetter));
                }
            }
            return showLetters;
        }

        public ShowLetter GetLetter(Guid id)
        {
            Flag = true;
            using (var content = new DefaultContext())
            {
                var letter = content.Letters.AsNoTracking().FirstOrDefault(x => x.ID == id);
                if (letter == null)
                {
                    return new ShowLetter();
                }
                return ShowLetter.CreateShowLetter(letter);
            }
        }

        public List<ShowLetter> GetLetter(DateTime beginTime, DateTime endTime)
        {
            Flag = true;
            using (var content = new DefaultContext())
            {
                var letters =
                    content.Letters.AsNoTracking().Where(x => x.CreateTime >= beginTime && x.CreateTime < endTime).ToList();
                if (letters.Null())
                {
                    return new List<ShowLetter>();
                }
                return letters.Select(x => ShowLetter.CreateShowLetter(x)).ToList();
            }
        }

        public void ReplyLetter(UpdateLetter updateLetter)
        {
            using (var content = new DefaultContext())
            {
                var letter = content.Letters.AsNoTracking().FirstOrDefault(x => x.ID == updateLetter.ID);
                if (letter == null)
                {
                    return;
                }
                letter.ReplyTime = DateTime.Now;
                letter.Reply = updateLetter.Message;
                content.ModifiedModel(letter);
                content.SaveChanges();
            }
            Flag = true;
        }

        public void Delete(List<Guid> letterIdList)
        {

            using (var context = new DefaultContext())
            {
                var letterList = context.Letters.Where(x => letterIdList.Contains(x.ID)).ToList();
                if (!letterList.Any())
                {
                    Message = "请选择正确的数据";
                    return;
                }
                context.Letters.RemoveRange(letterList);
                context.SaveChanges();
            }
            Flag = true;
        }

        public void SendLetterWcf(UpdateLetter updateLetter)
        {
            var binding = new NetMsmqBinding();

            binding.Security.Mode = NetMsmqSecurityMode.None;

            var endpointAddress = new EndpointAddress("net.msmq://10.2.0.172/private/Message");

            using (ChannelFactory<IMessageService> messageChannelFactory = new ChannelFactory<IMessageService>(binding, endpointAddress))
            {
                try
                {
                    IMessageService messageService = messageChannelFactory.CreateChannel();

                    messageService.GetTest(updateLetter);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            Flag = true;
        }
    }
}