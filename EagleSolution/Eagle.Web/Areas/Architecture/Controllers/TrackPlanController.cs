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
    public class TrackPlanController : Controller
    {
        // GET: Architecture/TrackPlan
        public ActionResult Index(int pageNum = 1)
        {
            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
            var trackPlans = trackPlanServices.Get(pageNum);
            ViewBag.totalPage = trackPlanServices.PageCount;
            return PartialView(model: new HtmlString(trackPlans.ToJson()));
        }

        // GET: Architecture/TrackPlan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Architecture/TrackPlan/Create
        public ActionResult Create(Guid? id)
        {
            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
            var updateTrackPlan = new UpdateTrackPlan();
            if (id.HasValue)
            {
                updateTrackPlan = trackPlanServices.Get(id.GetValueOrDefault());
            }
            else
            {
                updateTrackPlan.BeginTime = DateTime.Today;
                updateTrackPlan.EndTime = DateTime.Today.AddDays(1);
            }
            ViewBag.UpdateTrackPlan = new HtmlString(updateTrackPlan.ToJson());
            return PartialView();
        }

        // POST: Architecture/TrackPlan/Create
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

        // GET: Architecture/TrackPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Architecture/TrackPlan/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateTrackPlan updateTrackPlan)
        {
            try
            {

                var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
                if (updateTrackPlan.Null())
                {
                    var failure = trackPlanServices.GetResult();
                    return Json(failure);
                }
                trackPlanServices.Update(updateTrackPlan);
                var result = trackPlanServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(1);
            }
        }

        // GET: Architecture/TrackPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Architecture/TrackPlan/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {

            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
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

        public ActionResult Vote(Guid planId, Guid targetId, string userKey)
        {
            var trackPlanServices = ServiceLocator.Instance.GetService<ITrackPlanServices>();
            trackPlanServices.Vote(planId, targetId, userKey);
            return Content("");
        }
    }
}
