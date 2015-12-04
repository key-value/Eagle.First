using System.Web.Mvc;

namespace Eagle.Web.Two.Areas.WorkContent
{
    public class WorkContentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WorkContent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WorkContent_default",
                "WorkContent/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}