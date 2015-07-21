using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using Eagle.ViewModel.账户体系;

namespace Eagle.Web.Areas.Manage.Controllers
{
    public class BranchController : Controller
    {
        // GET: Manage/Branch
        public ActionResult Index(int pageNum = 1)
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var branchList = branchServices.GetBranches(pageNum);
            return PartialView(model: new HtmlString(branchList.ToJson()));
        }

        // GET: Manage/Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Branch/Create
        public ActionResult Create()
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var branchList = branchServices.GetFirstBranches();
            ViewBag.Branchs = new HtmlString(branchList.ToJson());
            return PartialView();
        }

        // POST: Manage/Branch/Create
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

        // GET: Manage/Branch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Branch/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateBranch branch)
        {
            try
            {
                // TODO: Add update logic here
                var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
                if (branch.Title.Null())
                {
                    var fault = branchServices.GetResult();
                    return Json(fault);
                }
                branchServices.Update(branch);
                var result = branchServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Branch/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Manage/Branch/Delete/5
        [HttpPost]
        public ActionResult Delete(List<string> id)
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
