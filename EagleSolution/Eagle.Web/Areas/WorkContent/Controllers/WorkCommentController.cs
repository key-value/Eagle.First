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
    public class WorkCommentController : Controller
    {
        // GET: WorkContent/WorkComment
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var showWorkRecords = workRecordServices.GetDepartment(userId, pageNum);
            ViewBag.totalPage = workRecordServices.PageCount;
            return PartialView(new HtmlString(showWorkRecords.ToJson()));
        }

        // GET: WorkContent/WorkComment/Details/5
        [HttpPost]
        public ActionResult Details(Guid? id)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var workComments = workRecordServices.GetWorkComments(id.GetValueOrDefault(), userId);
            return Json(workComments);
        }

        // GET: WorkContent/WorkComment/Create
        public ActionResult Create(Guid? id, Guid? recordId)
        {
            var userId = new Guid(User.Identity.Name);
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var updateWorkComment = new UpdateWorkComment();
            if (id.HasValue)
            {
                updateWorkComment = workRecordServices.GetWorkComment(userId, id.Value);
            }
            else
            {
                updateWorkComment.RecordId = recordId.GetValueOrDefault();
            }
            ViewBag.UpdateWorkComment = new HtmlString(updateWorkComment.ToJson());
            return PartialView();
        }

        // POST: WorkContent/WorkComment/Create
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

        // GET: WorkContent/WorkComment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/WorkComment/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateWorkComment updateWorkComment)
        {
            try
            {
                var userId = new Guid(User.Identity.Name);

                var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
                workRecordServices.UpdateComment(updateWorkComment, userId);
                var result = workRecordServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkContent/WorkComment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkContent/WorkComment/Delete/5
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
