using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.Interface;
using Eagle.ViewModel;

namespace Eagle.Web.Areas.WorkContent.Controllers
{
    public class WeeklyPlanController : Controller
    {
        // GET: WorkContent/WeeklyPlan
        public ActionResult Index(int pageNum = 1)
        {
            var userId = new Guid(User.Identity.Name);
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var currentWeeklyPlan = weeklyPlanServices.GetLastWeekPlan(userId);
            var weeklyPlanList = weeklyPlanServices.Get(userId, pageNum);
            ViewBag.WeekPlan = new HtmlString(currentWeeklyPlan.ToJson());
            ViewBag.totalPage = weeklyPlanServices.PageCount;
            ViewBag.WeekNum = DateTimeUtility.GetWeekOfYear(DateTime.Today);
            return PartialView(new HtmlString(weeklyPlanList.ToJson()));
        }

        [HttpPost]
        // GET: WorkContent/WeeklyPlan/Details/5
        public ActionResult Details(Guid? targetId)
        {
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var weeklyPlanList = new List<ShowWeekComent>() { new ShowWeekComent() { ConnectType = 0 } };
            weeklyPlanList.AddRange(weeklyPlanServices.GetShowWeekComents(targetId.GetValueOrDefault()));
            weeklyPlanList.Add(new ShowWeekComent() { ConnectType = 2 });

            return Json(weeklyPlanList);
        }

        // POST: WorkContent/WeeklyPlan/Create
        [HttpPost]
        public ActionResult Create(int id)
        {
            return View();
        }

        // GET: WorkContent/WeeklyPlan/Create
        public ActionResult Create()
        {
            var userId = new Guid(User.Identity.Name);
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var updateWeeklyTarget = new UpdateWeeklyTarget();

            updateWeeklyTarget = weeklyPlanServices.Get(userId);

            ViewBag.UpdateWeekTarget = new HtmlString(updateWeeklyTarget.ToJson());
            return PartialView();
        }

        // GET: WorkContent/WeeklyPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkContent/WeeklyPlan/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateWeeklyTarget updateWeeklyTarget)
        {
            try
            {
                // TODO: Add update logic here
                var userId = new Guid(User.Identity.Name);
                var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
                weeklyPlanServices.Update(updateWeeklyTarget, userId);
                var result = weeklyPlanServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetSummary()
        {
            var userId = new Guid(User.Identity.Name);
            var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
            var updateWeeklyTarget = new UpdateWeekSummary();

            updateWeeklyTarget = weeklyPlanServices.GetWeekSummary(userId);

            ViewBag.UpdateWeekSummarie = new HtmlString(updateWeeklyTarget.ToJson());
            return PartialView();
        }

        public ActionResult EditSummary(UpdateWeekSummary updateWeekSummary)
        {
            try
            {
                // TODO: Add update logic here
                var userId = new Guid(User.Identity.Name);
                var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
                weeklyPlanServices.UpdateSummary(updateWeekSummary);
                var result = weeklyPlanServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Content("1");
            }
        }

        public ActionResult GetWeekComments(Guid? commentId, Guid? targetId)
        {
            var updateWeekComment = new UpdateWeekComment();
            if (commentId.HasValue)
            {
                var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();

                updateWeekComment = weeklyPlanServices.GetWeekComments(commentId.GetValueOrDefault());
            }
            updateWeekComment.WeekTargetId = targetId.GetValueOrDefault();
            ViewBag.UpdateWeekComment = new HtmlString(updateWeekComment.ToJson());

            return PartialView();
        }

        public ActionResult EditWeekComments(UpdateWeekComment updateWeekComment)
        {
            try
            {
                // TODO: Add update logic here
                var userId = new Guid(User.Identity.Name);
                var weeklyPlanServices = ServiceLocator.Instance.GetService<IWeeklyPlanServices>();
                weeklyPlanServices.UpdateComment(updateWeekComment, userId);
                var result = weeklyPlanServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Content("1");
            }
        }
    }
}
