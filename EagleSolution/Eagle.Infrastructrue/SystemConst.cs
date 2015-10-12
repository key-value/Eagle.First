using System.Collections.Generic;
using System.Configuration;

namespace Eagle.Infrastructrue
{
    public static class SystemConst
    {
        static SystemConst()
        {
            //string myConn = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            //string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlConnectionString"].ToString();

        }
        public static string TimeStyle = "yy-MM-dd HH:mm:ss";

        public static string DefaultPassword = "123456";

        public static Dictionary<string, string> CateringSystem = new Dictionary<string, string>()
        {
            {"YumstoneV4","易石"},
            {"SHICHV60","石川V6.0"},
            {"SHICHV6","石川C6"},
            {"STTV6","思讯"},
            {"SHICHV3","石川C3"},
            {"QOrderV1","快单"},
            {"GenuineG6V7","正品贵德"},
            {"HD","互联餐厅"}
        };


        public static string GetCateringSystem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            if (CateringSystem.ContainsKey(name))
            {
                return CateringSystem[name];
            }
            return string.Empty;
        }
    }
}