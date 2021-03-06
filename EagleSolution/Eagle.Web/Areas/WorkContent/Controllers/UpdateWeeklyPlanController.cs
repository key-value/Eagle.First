﻿using System;
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
    public class UpdateWeeklyPlanController : Controller
    {
        // GET: WorkContent/UpdateWeeklyPlan
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var weeklyPlanList = weeklyPlanServices.Get(userId, pageNum);
            var currentWeeklyPlan = weeklyPlanList.FirstOrDefault();
            if (currentWeeklyPlan.Null())
            {
                ViewBag.WeekPlan = new HtmlString(new ShowWeeklyPlan().ToJson());
            }
            else
            {
                ViewBag.WeekPlan = new HtmlString(currentWeeklyPlan.ToJson());
            }
            ViewBag.WeekNum = DateTimeUtility.GetWeekOfYear(DateTime.Today);
            ViewBag.totalPage = weeklyPlanServices.PageCount;
            return PartialView(new HtmlString(weeklyPlanList.ToJson()));
        }

        // GET: WorkContent/UpdateWeeklyPlan/Details/5
        public ActionResult Details(Guid? targetId)
        {
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var weeklyPlanList = new List<ShowWeekComent>();
            var weeklyPlans = weeklyPlanServices.GetShowWeekComents(targetId.GetValueOrDefault());
            if (weeklyPlans.Any())
            {
                weeklyPlanList.Add(new ShowWeekComent() { ConnectType = 0 });
                weeklyPlanList.AddRange(weeklyPlans);
                weeklyPlanList.Add(new ShowWeekComent() { ConnectType = 2 });
            }
            else
            {
                weeklyPlanList.AddRange(weeklyPlans);
            }

            return Json(weeklyPlanList);
        }

        // GET: WorkContent/UpdateWeeklyPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkContent/UpdateWeeklyPlan/Create
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

        // GET: WorkContent/UpdateWeeklyPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/UpdateWeeklyPlan/Edit/5
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

        // GET: WorkContent/UpdateWeeklyPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkContent/UpdateWeeklyPlan/Delete/5
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
