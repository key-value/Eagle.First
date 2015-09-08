using Eagle.Infrastructrue.Aop.Locator;
using Eagle.Infrastructrue.Utility;
using Eagle.Server;
using Eagle.Server.Interface;
using Eagle.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Web.Areas.Submarine.Controllers
{
    public class CardController : Controller
    {
        // GET: Submarine/Card
        public ActionResult Index(int pageNum = 1)
        {
            var cardServices = ServiceLocator.Instance.GetService<ICardServices>();
            var showCards = cardServices.Get(pageNum);
            ViewBag.totalPage = cardServices.PageCount;
            return PartialView(model: new HtmlString(showCards.ToJson()));
        }

        // GET: Submarine/Card/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Submarine/Card/Create
        public ActionResult Create(int? id)
        {
            var branchServices = ServiceLocator.Instance.GetService<IBranchServices>();
            var branchList = branchServices.GetFirstBranches();
            ViewBag.Branchs = new HtmlString(branchList.ToJson());

            var cardServices = ServiceLocator.Instance.GetService<ICardServices>();
            var updateCard = new UpdateCard();
            if (id.HasValue)
            {
                updateCard = cardServices.GetCard(id.GetValueOrDefault());
            }
            ViewBag.UpdateSystemCard = new HtmlString(updateCard.ToJson());
            return PartialView();
        }

        // POST: Submarine/Card/Create
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

        // GET: Submarine/Card/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Submarine/Card/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateCard updateCard)
        {
            try
            {
                var cardServices = ServiceLocator.Instance.GetService<ICardServices>();
                if (updateCard.Null())
                {
                    var failure = cardServices.GetResult();
                    return Json(failure);
                }
                cardServices.Update(updateCard);
                var result = cardServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(1);
            }
        }

        // GET: Submarine/Card/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Submarine/Card/Delete/5
        [HttpPost]
        public ActionResult Delete(string[] id)
        {
            var cardServices = ServiceLocator.Instance.GetService<ICardServices>();
            var failt = cardServices.GetResult();
            try
            {
                if (id.Null() || !id.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }
                List<int> cardIdList = new List<int>();
                foreach (var bid in id)
                {
                    int brID;
                    if (!int.TryParse(bid, out  brID))
                    {
                        continue;
                    }
                    cardIdList.Add(brID);
                }
                if (!cardIdList.Any())
                {
                    failt.Message = "请选择要操作的数据";
                    return Json(failt);
                }

                cardServices.Delete(cardIdList);
                var result = cardServices.GetResult();
                return Json(result);
            }
            catch
            {
                return Json(failt);
            }
        }
    }
}
