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
    public class HeartbeatController : Controller
    {
        // GET: Architecture/Heartbeat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Architecture/Heartbeat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/Heartbeat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Architecture/Heartbeat/Create
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

        // GET: Architecture/Heartbeat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/Heartbeat/Edit/5
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

        // GET: Architecture/Heartbeat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/Heartbeat/Delete/5
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
        public ActionResult Collect(Guid machineId, string heartbeatBodys)
        {
            //var machineId = Guid.Empty;

            var heartbeatServices = ServiceLocator.Instance.GetService<IHeartbeatServices>();

            var heartbeatList = heartbeatBodys.ToDeserialize<List<HeartbeatBody>>();
            heartbeatServices.Add(heartbeatList, machineId);
            return Json(heartbeatServices.GetResult());
        }
    }
}
