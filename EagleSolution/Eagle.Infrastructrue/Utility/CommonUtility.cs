using System.Security.Cryptography;
using System.Text;

namespace Eagle.Infrastructrue.Utility
{
    public static class CommonUtility
    {
        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="obj">传进参数</param>
        /// <returns>返回加密后的东西</returns>
        public static string MD5(this string obj)
        {
            #region MD5加密
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(obj, "MD5");

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(obj));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
            #endregion
        }
        #endregion
    }
}