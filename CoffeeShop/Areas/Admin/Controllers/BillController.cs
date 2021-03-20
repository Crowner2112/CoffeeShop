using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BillDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult GetDetail(int id)
        {
            return RedirectToAction("Index", "BillDetail", new { id = id });
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var Bill = new BillDao().ViewDetail(id);
            return View(Bill);
        }
        [HttpPost]
        public ActionResult Create(Bill Bill)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDao();
                long id = dao.Insert(Bill);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Bill");
                }
                else
                {
                    ModelState.AddModelError("", "Add Bill successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Bill Bill)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDao();
                var result = dao.Update(Bill);
                if (result)
                {
                    return RedirectToAction("Index", "Bill");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Bill successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BillDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}