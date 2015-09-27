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
    /// 餐厅维护
    /// </summary>
    public class RestMaintainController : Controller
    {
        // GET: Brand/RestMaintain
        public ActionResult Index(Guid? cityId, string restName, int pageNum = 1)
        {
            var restaurantServices = ServiceLocator.Instance.GetService<IRestaurantServices>();
            var restList = restaurantServices.Get(cityId.GetValueOrDefault(), restName, 1, pageNum);
            ViewBag.totalPage = restaurantServices.PageCount;

            var cityServices = ServiceLocator.Instance.GetService<ICityServices>();
            var cityList = cityServices.GetCities();
            ViewBag.cityList = new HtmlString(cityList.ToJson());

            ViewBag.cityId = cityId;
            ViewBag.searchName = new HtmlString(restName);
            return PartialView(model: new HtmlString(restList.ToJson()));
        }

        [HttpPost]
        public ActionResult RestState(Guid? restId)
        {
            if (!restId.HasValue)
            {
                return Json(new Cells(false, "请选择正确的餐厅", 0));
            }
            var training = new Training(restId.GetValueOrDefault());
            training.Work();
            var result = training.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult UpLog(Guid? restId)
        {
            if (!restId.HasValue)
            {
                return Json(new Cells(false, "请选择正确的餐厅", 0));
            }
            var upLoadLog = new UpLoadLog(restId.GetValueOrDefault());
            upLoadLog.Work();
            var result = upLoadLog.GetResult();
            return Json(result);
        }

        [HttpPost]
        public ActionResult Restart(Guid? restId)
        {
            if (!restId.HasValue)
            {
                return Json(new Cells(false, "请选择正确的餐厅", 0));
            }
            var restartCommand = new RestartCommand(restId.GetValueOrDefault());
            restartCommand.Work();
            var result = restartCommand.GetResult();
            return Json(result);
        }
    }
}
