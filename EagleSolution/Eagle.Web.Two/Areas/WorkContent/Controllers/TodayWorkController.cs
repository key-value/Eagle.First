using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;

namespace Eagle.Web.Two.Areas.WorkContent.Controllers
{
    public class TodayWorkController : Controller
    {
        // GET: WorkContent/TodayWork
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid("85860B7B-4019-4E49-A98D-7FE287D09CDC");
            var workRecordServices = ServiceLocator.Instance.GetService<IWorkRecordServices>();
            var showWorkRecords = workRecordServices.Get(userId, pageNum);
            ViewBag.totalPage = workRecordServices.PageCount;
            return PartialView(new HtmlString(showWorkRecords.ToJson()));
        }
    }
}