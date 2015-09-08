using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Server;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Brand.Controllers
{
    public class BrandController : Controller
    {
        [HttpPost]
        public ActionResult InitRest()
        {
            var branchServices = ServiceLocator.Instance.GetService<IInitMonitorRestaruant>();
            branchServices.Init();
            var result = branchServices.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult InitCity()
        {
            var cityServices = ServiceLocator.Instance.GetService<ICityServices>();
            cityServices.Init();
            var result = cityServices.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult InitTable()
        {
            var tableServices = ServiceLocator.Instance.GetService<ITableServices>();
            tableServices.Init();
            var result = tableServices.GetResult();
            return Json(result);
        }
    }
}
