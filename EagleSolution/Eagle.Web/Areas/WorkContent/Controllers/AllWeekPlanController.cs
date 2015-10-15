using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.WorkContent.Controllers
{
    public class AllWeekPlanController : Controller
    {
        // GET: WorkContent/AllWeekPlan
        public ActionResult Index(int pageNum = 1)
        {
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var weeklyPlanList = weeklyPlanServices.Get(pageNum);
            ViewBag.WeekPlan = new HtmlString(new ShowWeeklyPlan().ToJson());
            ViewBag.totalPage = weeklyPlanServices.PageCount;
            return PartialView(new HtmlString(weeklyPlanList.ToJson()));
        }

        // GET: WorkContent/AllWeekPlan/Details/5
        public ActionResult Details(Guid? targetId)
        {
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var weekComents = weeklyPlanServices.GetShowWeekComents(targetId.GetValueOrDefault());
            var weeklyPlanList = new List<ShowWeekComent>();
            if (weekComents.Any())
            {
                weeklyPlanList.Add(new ShowWeekComent() { ConnectType = 0 });
                weeklyPlanList.AddRange(weeklyPlanServices.GetShowWeekComents(targetId.GetValueOrDefault()));
                weeklyPlanList.Add(new ShowWeekComent() { ConnectType = 2 });
            }

            return Json(weeklyPlanList);
        }

        // GET: WorkContent/AllWeekPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkContent/AllWeekPlan/Create
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

        // GET: WorkContent/AllWeekPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/AllWeekPlan/Edit/5
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

        // GET: WorkContent/AllWeekPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkContent/AllWeekPlan/Delete/5
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
