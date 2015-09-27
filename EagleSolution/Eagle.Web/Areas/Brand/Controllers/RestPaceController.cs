using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;

namespace Eagle.Web.Areas.Brand.Controllers
{
    public class RestPaceController : Controller
    {
        // GET: Brand/RestPace
        public ActionResult Index(int pageNum = 1)
        {
            var restaurantServices = ServiceLocator.Instance.GetService<IRestaurantServices>();
            var restList = restaurantServices.Get("1");
            ViewBag.rests = new HtmlString(restList.ToJson());
            var restPaceServices = ServiceLocator.Instance.GetService<IRestPaceServices>();
            var restpacs = restPaceServices.Get(pageNum);
            ViewBag.totalPage = restPaceServices.PageCount;
            return PartialView(new HtmlString(restpacs.ToJson()));
        }

        // GET: Brand/RestPace/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brand/RestPace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/RestPace/Create
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

        // GET: Brand/RestPace/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brand/RestPace/Edit/5
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

        // GET: Brand/RestPace/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brand/RestPace/Delete/5
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
