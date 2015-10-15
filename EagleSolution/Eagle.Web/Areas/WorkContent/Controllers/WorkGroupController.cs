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
    public class WorkGroupController : Controller
    {
        // GET: WorkContent/WorkGroup
        public ActionResult Index(Guid? depId, DateTime? selectTime)
        {
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
            var departments = new List<ShowDepartment>();
            departments.Add(new ShowDepartment() { Name = "全部" });
            departments.AddRange(departmentServices.Get());
            ViewBag.Department = new HtmlString(departments.ToJson());
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            if (selectTime.Null())
            {
                selectTime = DateTime.Now;
            }
            var showWorkRecords = workRecordServices.GetAllRecords(selectTime.GetValueOrDefault(), depId.GetValueOrDefault());
            ViewBag.DepId = depId.GetValueOrDefault();
            ViewBag.selectTime = selectTime;
            return PartialView(new HtmlString(showWorkRecords.ToJson()));
        }

        // GET: WorkContent/WorkGroup/Details/5
        public ActionResult Details(Guid? id)
        {
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
