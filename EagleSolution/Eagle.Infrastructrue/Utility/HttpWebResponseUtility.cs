using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NLog.Fluent;

namespace Eagle.Infrastructrue.Utility
{
    public class HttpWebResponseUtility
    {

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static string CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            string responseData = "";
            try
            {
                HttpWebResponse respone = request.GetResponse() as HttpWebResponse;
                StreamReader responseReader = null;
                responseReader = new StreamReader(respone.GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception)
            {
                return responseData;
            }
            return responseData;

        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }
        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <returns></returns>
        public static string CreatePostHttpResponse(string url, string parameters, int? timeout)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json";

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }

            byte[] data = Encoding.UTF8.GetBytes(parameters);

            string responseData = "";
            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse respone = request.GetResponse() as HttpWebResponse;
                StreamReader responseReader = null;
                responseReader = new StreamReader(respone.GetResponseStream());
                responseData = responseReader.ReadToEnd();
                responseReader.Close();
                respone.Close();
            }
            catch (Exception exception)
            {
                LogUtility.SendError(exception);
                return responseData;
            }
            return responseData;
        }
    }
}