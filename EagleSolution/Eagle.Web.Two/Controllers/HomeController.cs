using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using Microsoft.AspNet.Identity;

namespace Eagle.Web.Two.Controllers
{

	public class HomeController : Controller
	{

		public ActionResult Index()
		{
			var userId = new Guid("85860B7B-4019-4E49-A98D-7FE287D09CDC");
			var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
			var resultBranch = branchServices.GetBranchesByUser(userId);
			var accountServices = ServiceLocator.Instance.GetService<IAccountServices>();
			var account = accountServices.GetAccount(userId);
			ViewBag.Account = new HtmlString(account.ToJson());
			ViewBag.show = new HtmlString(resultBranch.ToJson());
			return View();
		}
	}
}