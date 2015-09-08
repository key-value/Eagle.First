using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server.Interface;
using System;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Submarine.Controllers
{
    public class NewbornController : Controller
    {
        // GET: Submarine/Newborn
        public ActionResult Index()
        {
            var userId = new Guid(User.Identity.Name);
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var resultBranch = branchServices.GetFirstBranchesAndCard(userId);
            return View(new HtmlString(resultBranch.ToJson()));
        }

    }
}
