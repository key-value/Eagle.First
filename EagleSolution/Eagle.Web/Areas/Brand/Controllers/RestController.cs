using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using System;
using System.Web;
using System.Web.Mvc;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.Brand.Controllers
{
    public class RestController : Controller
    {
        // GET: Brand/Rest
        public ActionResult Index(Guid? cityId, string restName, int pageNum = 1)
        {
            var restaurantServices = ServiceLocator.Instance.GetService<IRestaurantServices>();
            var restList = restaurantServices.GetAll(cityId.GetValueOrDefault(), restName, pageNum);
            ViewBag.totalPage = restaurantServices.PageCount;

            var cityServices = ServiceLocator.Instance.GetService<ICityServices>();
            var cityList = cityServices.GetCities();
            ViewBag.cityList = new HtmlString(cityList.ToJson());

            ViewBag.cityId = cityId;
            ViewBag.searchName = new HtmlString(restName);
            return PartialView(model: new HtmlString(restList.ToJson()));
        }

        public ActionResult Tables(Guid? restId, int pageNum)
        {
            if (!restId.HasValue)
            {
                return Content("");
            }
            var tableServices = ServiceLocator.Instance.GetService<ITableServices>();
            tableServices.PageSize = 12;
            var tables = tableServices.GetDeskList(restId.GetValueOrDefault(), pageNum);

            var cell = new SplinterCell<ShowTable>();
            cell.Data = tables;
            cell.PageCount = tableServices.PageCount;

            return Json(cell);
        }

        public ActionResult TableRecord(TableRecord tableRecord)
        {
            return Content("");
        }
    }
}
