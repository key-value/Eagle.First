using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using System;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Architecture.Controllers
{
    public class CpuChartController : Controller
    {
        // GET: Architecture/CpuChart
        public ActionResult Index(int pageNum)
        {
            var treeServices = ServiceLocator.Instance.GetService<ITreeServices>();
            var treeList = treeServices.Get(pageNum);
            return View(model: new HtmlString(treeList.ToJson()));
        }

        // GET: Architecture/CpuChart/Details/5
        public ActionResult Details(Guid treeId)
        {
            var heartbeatServices = ServiceLocator.Instance.GetService<IHeartbeatServices>();
            var heartbag = heartbeatServices.GetHeartbeatList(new DateTime(2015, 8, 8), treeId);
            return Content(heartbag.ToJson());
        }

        // GET: Architecture/CpuChart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Architecture/CpuChart/Create
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

        // GET: Architecture/CpuChart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/CpuChart/Edit/5
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

        // GET: Architecture/CpuChart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/CpuChart/Delete/5
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
