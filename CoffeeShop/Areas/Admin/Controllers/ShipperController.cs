using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class ShipperController : BaseController
    {
        // GET: Admin/Shipper
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ShipperDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Shipper Shipper)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShipperDao();
                long id = dao.Insert(Shipper);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Shipper");
                }
                else
                {
                    ModelState.AddModelError("", "Add Shipper successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Shipper Shipper)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShipperDao();
                var result = dao.Update(Shipper);
                if (result)
                {
                    return RedirectToAction("Index", "Shipper");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Shipper successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ShipperDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}