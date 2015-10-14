using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.Interface;

namespace Eagle.Web.Areas.WorkContent.Controllers
{
    public class WorkGroupController : Controller
    {
        // GET: WorkContent/WorkGroup
        public ActionResult Index(Guid? depId, int pageNum = 1)
        {
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
            var departments = departmentServices.Get();
            ViewBag.Department = new HtmlString(departments.ToJson());
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var showWorkRecords = workRecordServices.GetAllRecords(pageNum,depId.GetValueOrDefault());
            return PartialView(new HtmlString(showWorkRecords.ToJson()));
        }

        // GET: WorkContent/WorkGroup/Details/5
        public ActionResult Details(Guid? id)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var workComments = workRecordServices.GetWorkComments(id.GetValueOrDefault());
            return Json(workComments);
        }

        // GET: WorkContent/WorkGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkContent/WorkGroup/Create
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

        // GET: WorkContent/WorkGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/WorkGroup/Edit/5
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

        // GET: WorkContent/WorkGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkContent/WorkGroup/Delete/5
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
