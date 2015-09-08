using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using System;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Architecture.Controllers
{
    public class DiaryController : Controller
    {
        // GET: Architecture/Diary
        public ActionResult Index(int pageNum = 1)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var journalList = journalServices.GetJournals(pageNum);
            return PartialView(model: new HtmlString(journalList.ToJson()));
        }

        // GET: Architecture/Diary/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/Diary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Architecture/Diary/Create
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

        // GET: Architecture/Diary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/Diary/Edit/5
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

        // GET: Architecture/Diary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/Diary/Delete/5
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

        [HttpPost]
        public ActionResult GetFiles(Guid journalId, string path)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var fileList = journalServices.GetFiles(journalId, path);
            if (!string.IsNullOrEmpty(path))
            {
                fileList.Insert(0, "..");
            }
            return Json(fileList);
        }


        [HttpPost]
        public ActionResult GetFile(Guid journalId, string fileName)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var fileList = journalServices.GetFile(journalId, fileName);

            return Content(fileList);
        }
    }
}
