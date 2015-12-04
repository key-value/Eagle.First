using System;
using System.Threading.Tasks;
using Eagle.Infrastructrue.Utility;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Eagle.Web.Two.App_Start.Startup))]

namespace Eagle.Web.Two.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.Use((context, func) =>
            {
                var path = context.Request.Path.Value;
                if (!string.IsNullOrEmpty(path) && !path.Contains(".") && !path.StartsWith("/_"))
                {
                    var queryString = context.Request.QueryString.Value;
                    var remoteIpAddress = context.Request.RemoteIpAddress;
                    var statusCode = context.Response.StatusCode;
                    LogUtility.SendFatal($"访问路径{path},参数{queryString},远端主机{remoteIpAddress},状态码 {statusCode}");
                }
                func.Invoke();
                return context.Response.WriteAsync("");
            });
        }
    }
}
