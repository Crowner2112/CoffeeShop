using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
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
            var Order = new OrderDao().ViewDetail(id);
            return View(Order);
        }
        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                long id = dao.Insert(order);
                if (id > 0)
                {
                    return RedirectToAction("Index", "OrderDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Add Order Detail successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                var result = dao.Update(order);
                if (result)
                {
                    return RedirectToAction("Index", "OrderDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Modify successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}