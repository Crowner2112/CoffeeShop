using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/News
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new NewsDao();
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
            var News = new NewsDao().ViewDetail(id);
            return View(News);
        }
        [HttpPost]
        public ActionResult Create(News News)
        {
            if (ModelState.IsValid)
            {
                var dao = new NewsDao();
                var id = dao.Insert(News);
                if (id > 0)
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", "Add News successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(News News)
        {
            if (ModelState.IsValid)
            {
                var dao = new NewsDao();
                var result = dao.Update(News);
                if (result)
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", "Modify News successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new NewsDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}