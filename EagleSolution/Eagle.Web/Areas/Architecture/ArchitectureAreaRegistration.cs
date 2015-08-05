using System.Web.Mvc;

namespace Eagle.Web.Areas.Architecture
{
    public class ArchitectureAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Architecture";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Architecture_default",
                "Architecture/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}