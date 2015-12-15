using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Application.Server.ApiServer;
using Eagle.ViewModel;

namespace Eagle.Web.Two.Controllers
{
    public class ChannelController : Controller
    {
        // GET: Channel
        [HttpPost]
        public ActionResult Index(string actionName, string areaName, Dictionary<string, string> dic = null)
        {
            var visitor = new Visitor(areaName, actionName, new Guid(User.Identity.Name));

            var result = visitor.Parser(dic);

            return Json(result);

        }
    }
}