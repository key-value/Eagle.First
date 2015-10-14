using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using Eagle.ViewModel;
using Microsoft.Data.Edm.Expressions;

namespace Eagle.Web.Areas.Manage.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Manage/Department
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
            var departments = departmentServices.Get(pageNum);
            ViewBag.totalPage = departmentServices.PageCount;
            var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
            var accounts = accountServices.GetAccounts(userId);
            ViewBag.Accounts = new HtmlString(accounts.ToJson());
            return PartialView(new HtmlString(departments.ToJson()));
        }

        // GET: Manage/Department/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Manage/Department/Create
        public ActionResult Create(Guid? id)
        {
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
            var updateDepartment = new ShowDepartment();
            var accounts = new List<ShowAccount>();
            var ownerId = Guid.Empty;
            if (id.HasValue)
            {
                updateDepartment = departmentServices.Get(id.Value);
                var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
                accounts = accountServices.GetAccountsByDepartment(id.Value);
                updateDepartment.OwnerId = departmentServices.GetOwner(id.Value);
            }
            ViewBag.ShowAccounts = new HtmlString(accounts.ToJson());
            ViewBag.updateDepartment = new HtmlString(updateDepartment.ToJson());
            return PartialView();
        }

        // POST: Manage/Department/Create
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

        // GET: Manage/Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Department/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateDepartment updateDepartment)
        {
            try
            {
                // TODO: Add update logic here
                var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
                if (updateDepartment.Name.Null())
                {
                    var failure = departmentServices.GetResult();
                    return Json(failure);
                }
                departmentServices.Update(updateDepartment);
                var result = departmentServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Department/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();

            var failt = departmentServices.GetResult();
            try
            {
                if (id.Null() || !id.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                List<Guid> guids = new List<Guid>();
                foreach (var bid in id)
                {
                    Guid brID;
                    if (!Guid.TryParse(bid, out brID))
                    {
                        continue;
                    }
                    guids.Add(brID);
                }
                if (!guids.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                // TODO: Add delete logic here
                departmentServices.Delete(guids);
                var result = departmentServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }

        [HttpPost]
        public ActionResult GetWorkCard(Guid? id)
        {
            var departmentServices = ServiceLocator.Instance.GetService<IDepartmentServices>();
            var cardidlist = departmentServices.GetWorkCard(id.GetValueOrDefault());
            return Content(content: cardidlist.ToJson());
        }
    }
}
