using System.Web.Mvc;

namespace Eagle.Web.Areas.Submarine
{
    public class SubmarineAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Submarine";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Submarine_default",
                "Submarine/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}