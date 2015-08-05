using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Architecture.Controllers
{
    public class JournalController : Controller
    {
        // GET: Architecture/Journal
        public ActionResult Index()
        {
            return View();
        }

        // GET: Architecture/Journal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/Journal/Create
        public ActionResult Create()
        {
            return View();
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

        // GET: Architecture/Journal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/Journal/Delete/5
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
