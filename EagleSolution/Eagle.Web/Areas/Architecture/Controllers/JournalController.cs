using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.Architecture.Controllers
{
    public class JournalController : Controller
    {
        // GET: Architecture/Journal
        public ActionResult Index(int pageNum)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var journalList = journalServices.GetJournals(pageNum);
            return PartialView(model: new HtmlString(journalList.ToJson()));
        }

        // GET: Architecture/Journal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/Journal/Create
        public ActionResult Create(Guid? id)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var journal = new UpdateJournal();
            if (id.HasValue)
            {
                journal = journalServices.Get(id.GetValueOrDefault());
            }
            ViewBag.UpdateJournal = new HtmlString(journal.ToJson());
            return PartialView();
        }

        // POST: Architecture/Journal/Create
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

        // GET: Architecture/Journal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/Journal/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateJournal updateJournal)
        {
            try
            {
                var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
                if (updateJournal.Null())
                {
                    var failure = journalServices.GetResult();
                    return Json(failure);
                }
                journalServices.Update(updateJournal);
                var result = journalServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(1);
            }
        }

        // GET: Architecture/Journal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/Journal/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {
            var journalServices = ServiceLocator.Instance.GetService<IJournalServices>();
            var failt = journalServices.GetResult();
            try
            {
                if (id.Null() || !id.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                List<Guid> branchIdList = new List<Guid>();
                foreach (var bid in id)
                {
                    Guid brID;
                    if (!Guid.TryParse(bid, out  brID))
                    {
                        continue;
                    }
                    branchIdList.Add(brID);
                }
                if (!branchIdList.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }

                journalServices.Delete(branchIdList);
                var result = journalServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }
    }
}
