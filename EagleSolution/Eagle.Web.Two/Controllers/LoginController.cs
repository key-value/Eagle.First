using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;

namespace Eagle.Web.Two.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
#if DEBUG
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var account = accountServices.Login("diao", "123");

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, account.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, account.ToJson(), FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie newCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(newCookie);
           return RedirectToAction("Index", "Home");
#endif


            return View();
        }

        // GET: Account/Details/5
        [HttpPost]
        public ActionResult In(string accountName, string password)
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
    }
}
