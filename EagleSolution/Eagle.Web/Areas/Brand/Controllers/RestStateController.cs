using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.SockCommand;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.Brand.Controllers
{
    /// <summary>
    /// 餐厅状态
    /// </summary>
    public class RestStateController : Controller
    {
        // GET: Brand/RestState
        public ActionResult Index(Guid? cityId, string restName, int pageNum = 1)
        {
            var restaurantServices = ServiceLocator.Instance.GetService<IRestaurantServices>();
            var restList = restaurantServices.Get(cityId.GetValueOrDefault(), restName, 0, pageNum);
            ViewBag.totalPage = restaurantServices.PageCount;

            var cityServices = ServiceLocator.Instance.GetService<ICityServices>();
            var cityList = cityServices.GetCities();
            ViewBag.cityList = new HtmlString(cityList.ToJson());

            ViewBag.cityId = cityId;
            ViewBag.searchName = new HtmlString(restName);
            return PartialView(model: new HtmlString(restList.ToJson()));
        }

        public ActionResult ConnectSteps(Guid? restId, int pageNum)
        {
            if (!restId.HasValue)
            {
                return Content("");
            }
            var monitorConnectStepServices = ServiceLocator.Instance.GetService<IMonitorConnectStepServices>();
            var monitorConnectSteps = monitorConnectStepServices.GetMonitorConnectSteps(restId.GetValueOrDefault(), pageNum);

            var cell = new SplinterCell<ShowStep>();
            cell.Data = monitorConnectSteps;
            cell.PageCount = monitorConnectStepServices.PageCount;

            return Json(cell);
        }

        [HttpPost]
        public ActionResult UpdateConnectStep(UpdateConnectStep updateConnectStep)
        {
            var monitorConnectStepServices = ServiceLocator.Instance.GetService<IMonitorConnectStepServices>();
            monitorConnectStepServices.UpdateConnectStep(updateConnectStep);

            var result = monitorConnectStepServices.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult RefreshRest(string restIds)
        {
            var monitorConnectStepServices = ServiceLocator.Instance.GetService<IMonitorConnectStepServices>();
            if (string.IsNullOrEmpty(restIds))
            {
                return Json(monitorConnectStepServices.GetResult());
            }
            var restList = restIds.ToDeserialize<List<Guid>>();
            if (!restList.Any())
            {
                return Json(monitorConnectStepServices.GetResult());
            }
            monitorConnectStepServices.RefreshRestaurantState(restList);

            var result = monitorConnectStepServices.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult RefreshRestAction()
        {
            var refleshRestCommand = new RefleshRestCommand();
            refleshRestCommand.Work();
            var result = refleshRestCommand.GetResult();
            return Json(result);
        }
    }
}
