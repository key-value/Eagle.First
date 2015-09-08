using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using Eagle.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Manage.Controllers
{
    public class BranchController : Controller
    {
        // GET: Manage/Branch
        public ActionResult Index(int pageNum = 1)
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var branchList = branchServices.GetBranches(pageNum);
            ViewBag.totalPage = branchServices.PageCount;
            return PartialView(model: new HtmlString(branchList.ToJson()));
        }

        // GET: Manage/Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Branch/Create
        public ActionResult Create(Guid? id)
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var branchList = branchServices.GetFirstBranches();
            ViewBag.Branchs = new HtmlString(branchList.ToJson());
            var branchs = new UpdateBranch();
            if (id.HasValue)
            {
                branchs = branchServices.GetBranches(id.Value);
            }
            ViewBag.UpdateBranch = new HtmlString(branchs.ToJson());
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
                    var failure = branchServices.GetResult();
                    return Json(failure);
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
        public ActionResult Delete(string[] id)
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();

            var failt = branchServices.GetResult();
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
                // TODO: Add delete logic here
                branchServices.Delete(branchIdList);
                var result = branchServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }
    }
}
