using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Message.Controllers
{
    public class LetterController : Controller
    {
        // GET: Message/Letter
        public ActionResult Index(int pageNum = 1)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();
            var letterList = letterServices.GetLetters(pageNum);
            return View(model: new HtmlString(letterList.ToJson()));
        }
        // GET: Message/Letter
        [HttpPost]
        public ActionResult Index(DateTime beginTime, DateTime endTime)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();
            var letterList = letterServices.GetLetter(beginTime, endTime);
            return Json(letterList);
        }

        // GET: Message/Letter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Letter/Create
        public ActionResult Create(Guid? id)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();
            var letter = new ShowLetter();
            if (id.HasValue)
            {
                letter = letterServices.GetLetter(id.Value);
            }
            ViewBag.UpdateLetter = new HtmlString(letter.ToJson());
            return PartialView();
        }

        // POST: Message/Letter/Create
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

        // GET: Message/Letter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Letter/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateLetter updateLetter)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();
            try
            {
                // TODO: Add update logic here
                updateLetter.ID = Guid.NewGuid();
                letterServices.SendLetterWcf(updateLetter);
                var result = letterServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(0);
            }
        }

        // GET: Message/Letter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Letter/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();

            var failt = letterServices.GetResult();
            try
            {
                if (id.Null() || !id.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                List<Guid> letterIdList = new List<Guid>();
                foreach (var bid in id)
                {
                    Guid brID;
                    if (!Guid.TryParse(bid, out  brID))
                    {
                        continue;
                    }
                    letterIdList.Add(brID);
                }
                if (!letterIdList.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }

                letterServices.Delete(letterIdList);
                var result = letterServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }

        [HttpPost]
        public ActionResult Reply(UpdateLetter updateLetter)
        {
            var letterServices = ServiceLocator.Instance.GetService<ILetterServices>();
            try
            {
                // TODO: Add update logic here
                letterServices.ReplyLetter(updateLetter);
                var result = letterServices.GetResult();
                return Json(result);
            }
            catch
            {
                return View();
            }
        }
    }
}
