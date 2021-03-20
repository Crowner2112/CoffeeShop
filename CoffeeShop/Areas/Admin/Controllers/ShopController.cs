using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Admin/Shop
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ShopDao();
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
            var Shop = new ShopDao().ViewDetail(id);
            return View(Shop);
        }
        [HttpPost]
        public ActionResult Create(Shop Shop)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShopDao();
                var id = dao.Insert(Shop);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Shop");
                }
                else
                {
                    ModelState.AddModelError("", "Add Shop successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Shop Shop)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShopDao();
                var result = dao.Update(Shop);
                if (result)
                {
                    return RedirectToAction("Index", "Shop");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Shop successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ShopDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
