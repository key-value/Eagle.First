using System.Web;
using System.Web.Mvc;
using Eagle.Web.Two.Expand;

namespace Eagle.Web.Two
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());


            filters.Add(new LogExceptionFilterAttribute());
        }
    }
}
