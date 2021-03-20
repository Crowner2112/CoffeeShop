using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDetailDao();
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
            var Order = new OrderDetailDao().ViewDetail(id);
            return View(Order);
        }
        [HttpPost]
        public ActionResult Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDetailDao();
                long id = dao.Insert(orderDetail);
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
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDetailDao();
                var result = dao.Update(orderDetail);
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
            new OrderDetailDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}