using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using System;
using System.Web;
using System.Web.Mvc;
using Eagle.ViewModel;


namespace Eagle.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            var userId = new Guid(User.Identity.Name);
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var resultBranch = branchServices.GetBranchesByUser(userId);
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var account = accountServices.GetAccount(userId);
            ViewBag.Account = new HtmlString(account.ToJson());
            ViewBag.show = new HtmlString(resultBranch.ToJson());
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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

        public ActionResult GetPassword()
        {
            var userId = new Guid(User.Identity.Name);
            return PartialView(new HtmlString(new ChangeAccount() { ID = userId }.ToJson()));
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangeAccount changeAccount)
        {
            var userId = new Guid(User.Identity.Name);
            changeAccount.ID = userId;
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            accountServices.ChangePassword(changeAccount);
            var result = accountServices.GetResult();
            return Json(result);
        }
    }
}
