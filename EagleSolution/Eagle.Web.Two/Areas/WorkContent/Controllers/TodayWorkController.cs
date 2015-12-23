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
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}