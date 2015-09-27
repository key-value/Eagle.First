using System;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Server;
using System.Web.Mvc;
using Eagle.ViewModel;

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


        [HttpPost]
        public ActionResult UpdateConnectStep()
        {
            var updateConnectStep = new UpdateConnectStep();
            updateConnectStep.ID = Guid.NewGuid();
            updateConnectStep.RestaurantId = Guid.Empty;

            updateConnectStep.SockCreateTime = DateTime.Now;
            updateConnectStep.SockSendTime = DateTime.Now;
            updateConnectStep.ConnectResult = 1;
            updateConnectStep.ConnectType = 1;
            updateConnectStep.IpAddress = "192.168.1.1";
            var monitorConnectStepServices = ServiceLocator.Instance.GetService<IMonitorConnectStepServices>();
            monitorConnectStepServices.UpdateConnectStep(updateConnectStep);

            var result = monitorConnectStepServices.GetResult();
            return Json(result);
        }
    }
}
