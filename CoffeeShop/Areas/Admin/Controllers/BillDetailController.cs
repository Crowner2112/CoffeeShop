using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class BillDetailController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BillDetailDao();
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
            var BillDetail = new BillDetailDao().ViewDetail(id);
            return View(BillDetail);
        }
        [HttpPost]
        public ActionResult Create(BillDetail BillDetail)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDetailDao();
                long id = dao.Insert(BillDetail);
                if (id > 0)
                {
                    return RedirectToAction("Index", "BillDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Add BillDetail successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(BillDetail BillDetail)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDetailDao();
                var result = dao.Update(BillDetail);
                if (result)
                {
                    return RedirectToAction("Index", "BillDetail");
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
            new BillDetailDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}