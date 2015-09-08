using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Eagle.Web.Controllers
{
    public class RiceController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        [HttpPost]
        public ActionResult Login(string accountName, string password)
        {
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            try
            {
                var account = accountServices.Login(accountName, password);
                if (!accountServices.Flag)
                {
                    return Json(accountServices.GetResult());
                }

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, account.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, account.ToJson(), FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie newCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(newCookie);
                return Json(accountServices.GetResult());
            }
            catch (Exception ex)
            {
                var result = accountServices.GetResult();
                result.Message = ex.Message;
                return Json(result);
            }


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
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

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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
