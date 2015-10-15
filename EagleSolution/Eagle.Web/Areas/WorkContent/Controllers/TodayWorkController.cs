using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.Interface;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.WorkContent.Controllers
{
    public class TodayWorkController : Controller
    {
        // GET: WorkContent/TodayWork
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var showWorkRecords = workRecordServices.Get(userId, pageNum);
            ViewBag.totalPage = workRecordServices.PageCount;
            return PartialView(new HtmlString(showWorkRecords.ToJson()));
        }

        // GET: WorkContent/TodayWork/Details/5
        public ActionResult Details(Guid? id)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var workComments = workRecordServices.GetWorkComments(id.GetValueOrDefault(), userId);
            return Json(workComments);
        }

        // GET: WorkContent/TodayWork/Create
        public ActionResult Create(Guid? id)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var updateWorkRecord = new UpdateWorkRecord();
            if (id.HasValue)
            {
                updateWorkRecord = workRecordServices.Get(userId, id.Value);
            }
            ViewBag.UpdateWorkRecord = new HtmlString(updateWorkRecord.ToJson());
            return PartialView();
        }

        // POST: WorkContent/TodayWork/Create
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

        // GET: WorkContent/TodayWork/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/TodayWork/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateWorkRecord updateWorkRecord)
        {
            try
            {
                var userId = new Guid(User.Identity.Name);
                // TODO: Add update logic here
                var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
                workRecordServices.Update(updateWorkRecord, userId);
                var result = workRecordServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkContent/TodayWork/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkContent/TodayWork/Delete/5
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
