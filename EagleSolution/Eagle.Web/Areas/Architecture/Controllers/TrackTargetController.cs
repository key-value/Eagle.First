using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.Architecture.Controllers
{
    public class TrackTargetController : Controller
    {
        // GET: Architecture/TrackTarget
        public ActionResult Index(int pageNum = 1)
        {
            var trackTargetServices = ServiceLocator.Instance.GetService<ITrackTargetServices>();
            var trackPlans = trackTargetServices.Get(pageNum);
            ViewBag.totalPage = trackTargetServices.PageCount;
            return PartialView(model: new HtmlString(trackPlans.ToJson()));
        }

        // GET: Architecture/TrackTarget/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/TrackTarget/Create
        public ActionResult Create(Guid? id)
        {
            var trackTargetServices = ServiceLocator.Instance.GetService<ITrackTargetServices>();
            var updateTrackTarget = new UpdateTrackTarget();
            if (id.HasValue)
            {
                updateTrackTarget = trackTargetServices.Get(id.GetValueOrDefault());
            }
            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
            var trackPlan = trackPlanServices.Get();
            ViewBag.TrackPlans = new HtmlString(trackPlan.ToJson());
            ViewBag.UpdateTrackTarget = new HtmlString(updateTrackTarget.ToJson());
            return PartialView();
        }

        // POST: Architecture/TrackTarget/Create
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

        // GET: Architecture/TrackTarget/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/TrackTarget/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateTrackTarget updateTrackTarget)
        {
            try
            {
                var trackTargetServices = ServiceLocator.Instance.GetService<ITrackTargetServices>();
                if (updateTrackTarget.Null())
                {
                    var failure = trackTargetServices.GetResult();
                    return Json(failure);
                }
                trackTargetServices.Update(updateTrackTarget);
                var result = trackTargetServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(1);
            }
        }

        // GET: Architecture/TrackTarget/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/TrackTarget/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {
            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackTargetServices>();
            var failt = trackPlanServices.GetResult();

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

                trackPlanServices.Delete(guids);
                var result = trackPlanServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }

        [HttpPost]
        public void Vote(string userKey, Guid targetId)
        {
            var trackTargetServices = ServiceLocator.Instance.GetService<ITrackTargetServices>();
            if (targetId.Null() || userKey.Null())
            {
                return;
            }
            trackTargetServices.Vote(userKey, targetId);

        }
    }
}
