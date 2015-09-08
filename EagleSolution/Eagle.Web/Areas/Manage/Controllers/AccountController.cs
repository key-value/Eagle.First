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
    public class AccountController : Controller
    {
        // GET: Manage/Account
        public ActionResult Index(int pageNum = 1)
        {
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var accountList = accountServices.GetAccounts(pageNum);
            ViewBag.totalPage = accountServices.PageCount;
            return View(model: new HtmlString(accountList.ToJson()));
        }

        // GET: Manage/Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Account/Create
        public ActionResult Create(Guid? id)
        {
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var updateAccount = new UpdateAccount();
            if (id.HasValue)
            {
                updateAccount = accountServices.GetAccount(id.Value);
            }
            ViewBag.UpdateAccount = new HtmlString(updateAccount.ToJson());
            return PartialView();
        }

        // POST: Manage/Account/Create
        [HttpPost]
        public ActionResult Create(Guid id)
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
        public ActionResult Edit(UpdateAccount updateAccount)
        {
            try
            {
                // TODO: Add update logic here
                var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
                accountServices.Update(updateAccount);
                var result = accountServices.GetResult();
                return Json(result);
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
        public ActionResult Delete(string[] id)
        {
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var failt = accountServices.GetResult();
            try
            {
                // TODO: Add delete logic here

                if (id.Null() || !id.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                List<Guid> accountIdList = new List<Guid>();
                foreach (var aid in id)
                {
                    Guid accountId;
                    if (!Guid.TryParse(aid, out accountId))
                    {
                        continue;
                    }
                    accountIdList.Add(accountId);
                }
                if (!accountIdList.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                accountServices.Delete(accountIdList);
                var result = accountServices.GetResult();
                return Json(failt);
            }
            catch
            {
                return Json(failt);
            }
        }
    }
}
