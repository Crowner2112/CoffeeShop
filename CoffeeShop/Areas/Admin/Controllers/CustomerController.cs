using Models.DAO;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CustomerDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CustomerDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}