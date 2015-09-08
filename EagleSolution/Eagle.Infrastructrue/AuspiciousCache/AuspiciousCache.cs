using System.Configuration;

namespace Eagle.Infrastructrue.AuspiciousCache
{
    public static class AuspiciousCache
    {
        static AuspiciousCache()
        {
            AuspiciousIp = ConfigurationManager.AppSettings["AuspiciousIp"];
            var auspiciousPort = ConfigurationManager.AppSettings["AuspiciousPort"];
            int.TryParse(auspiciousPort, out AuspiciousPort);
            MonitorDatabase = ConfigurationManager.AppSettings["MonitorDatabase"];
        }

        public static string AuspiciousIp;

        public static int AuspiciousPort;

        public static string MonitorDatabase;
    }
}