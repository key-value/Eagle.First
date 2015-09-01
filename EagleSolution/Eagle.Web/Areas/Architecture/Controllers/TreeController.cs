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
    public class TreeController : Controller
    {
        // GET: Architecture/Tree
        public ActionResult Index(int pageNum = 1)
        {
            var treeServices = ServiceLocator.Instance.GetService<ITreeServices>();
            var treelist = treeServices.Get(pageNum);
            ViewBag.totalPage = treeServices.PageCount;
            return PartialView(model: new HtmlString(treelist.ToJson()));
        }

        // GET: Architecture/Tree/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/Tree/Create
        public ActionResult Create(Guid? id)
        {
            var treeServices = ServiceLocator.Instance.GetService<ITreeServices>();
            var tree = new UpdateTree();
            if (id.HasValue)
            {
                tree = treeServices.Get(id.GetValueOrDefault());
            }
            ViewBag.UpdateTree = new HtmlString(tree.ToJson());
            return PartialView();
        }

        // POST: Architecture/Tree/Create
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

        // GET: Architecture/Tree/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/Tree/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateTree updateTree)
        {
            try
            {

                var treeServices = ServiceLocator.Instance.GetService<ITreeServices>();
                if (updateTree.Null())
                {
                    var failure = treeServices.GetResult();
                    return Json(failure);
                }
                treeServices.Update(updateTree);
                var result = treeServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(1);
            }
        }

        // GET: Architecture/Tree/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/Tree/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {

            var treeServices = ServiceLocator.Instance.GetService<ITreeServices>();
            var failt = treeServices.GetResult();

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

                treeServices.Delete(branchIdList);
                var result = treeServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }
    }
}
