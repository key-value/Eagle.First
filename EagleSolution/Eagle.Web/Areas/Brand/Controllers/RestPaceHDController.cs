using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Brand.Controllers
{
    public class RestPaceHDController : Controller
    {
        // GET: Brand/RestPaceHD
        public ActionResult Index()
        {
            return View();
        }

        // GET: Brand/RestPaceHD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brand/RestPaceHD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/RestPaceHD/Create
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

        // GET: Brand/RestPaceHD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brand/RestPaceHD/Edit/5
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

        // GET: Brand/RestPaceHD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brand/RestPaceHD/Delete/5
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
