using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Manage.Controllers
{
    public class JurisdictionController : Controller
    {
        // GET: Manage/Jurisdiction
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var account = accountServices.GetAccounts(pageNum, userId);
            ViewBag.totalPage = accountServices.PageCount;
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            ViewBag.branchs = new HtmlString(branchServices.GetBranches().ToJson());
            //ViewBag.jurisdc = new HtmlString(new Dictionary<string, string>().ToJson());
            return PartialView(new HtmlString(account.ToJson()));
        }

        // GET: Manage/Jurisdiction/Details/5
        [HttpPost]
        public ActionResult Details(Guid accountId)
        {
            var jurisdictionServices = ServiceLocator.Instance.GetService<IJurisdictionServices>();
            var jurisdictions = jurisdictionServices.GetJurisdiction(accountId);
            return Content(content: jurisdictions.ToJson());
        }

        // GET: Manage/Jurisdiction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Jurisdiction/Create
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

        // GET: Manage/Jurisdiction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Jurisdiction/Edit/5
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

        // GET: Manage/Jurisdiction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Jurisdiction/Delete/5
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
        public ActionResult Distribution(Guid accountId, string[] addList, string[] delList)
        {
            var jurisdictionServices = ServiceLocator.Instance.GetService<IJurisdictionServices>();
            var addAccounts = new List<Guid>();
            var delAccounts = new List<Guid>();
            if (!addList.Null())
            {
                foreach (var addid in addList)
                {
                    Guid accountid;
                    if (Guid.TryParse(addid, out accountid))
                    {
                        addAccounts.Add(accountid);
                    }
                }
            }
            if (!delList.Null())
            {
                foreach (var addid in delList)
                {
                    Guid accountid;
                    if (Guid.TryParse(addid, out accountid))
                    {
                        delAccounts.Add(accountid);
                    }
                }
            }
            jurisdictionServices.Distribution(accountId, addAccounts, delAccounts);
            return Json(jurisdictionServices.GetResult());
        }
    }
}
