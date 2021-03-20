using Models.DAO;
using Models.EF;
using System;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
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
            var Product = new ProductDao().ViewDetail(id);
            return View(Product);
        }
        [HttpPost]
        public ActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                Product.CreateDate = DateTime.Now;
                long id = dao.Insert(Product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Add product successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(Product);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Modify product successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}