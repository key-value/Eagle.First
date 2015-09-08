using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using System;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Brand.Controllers
{
    public class RestController : Controller
    {
        // GET: Brand/Rest
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

        // GET: Brand/Rest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brand/Rest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Rest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brand/Rest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brand/Rest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brand/Rest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brand/Rest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
