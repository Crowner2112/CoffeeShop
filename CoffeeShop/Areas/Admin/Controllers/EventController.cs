using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class EventController : BaseController
    {
        // GET: Admin/Event
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new EventDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var Event = new EventDao().ViewDetail(id);
            return View(Event);
        }
        [HttpPost]
        public ActionResult Create(Event Event)
        {
            if (ModelState.IsValid)
            {
                var dao = new EventDao();
                var id = dao.Insert(Event);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Event");
                }
                else
                {
                    ModelState.AddModelError("", "Add Event successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Event Event)
        {
            if (ModelState.IsValid)
            {
                var dao = new EventDao();
                var result = dao.Update(Event);
                if (result)
                {
                    return RedirectToAction("Index", "Event");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Event successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new EventDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}