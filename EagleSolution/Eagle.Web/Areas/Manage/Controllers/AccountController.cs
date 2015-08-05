using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;

namespace Eagle.Web.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        // GET: Manage/Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Account/Create
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

        // GET: Manage/Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Account/Edit/5
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

        // GET: Manage/Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Account/Delete/5
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
