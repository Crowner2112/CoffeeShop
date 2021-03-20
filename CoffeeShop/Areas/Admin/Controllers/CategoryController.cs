using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
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
            var Category = new CategoryDao().ViewDetail(id);
            return View(Category);
        }
        [HttpPost]
        public ActionResult Create(Category Category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                long id = dao.Insert(Category);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Add Category successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Category Category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var result = dao.Update(Category);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Category successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}