using System.Collections.Generic;
using System.Linq;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Server.Services
{
    public class JournalServices : IJournalServices
    {
        public List<string> GetJournal()
        {
            var resString = HttpWebResponseUtility.CreateGetHttpResponse("http://second.eagle.com/api/Message", 3000, null, null);

            var result = resString.ToDeserialize<List<string>>() ?? new List<string>();

            return result;
        }
    }
}