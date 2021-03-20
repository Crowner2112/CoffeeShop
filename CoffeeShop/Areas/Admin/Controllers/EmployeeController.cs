using CoffeeShop.Common;
using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Admin/Employee
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new EmployeeDao();
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
            var employee = new EmployeeDao().ViewDetail(id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var dao = new EmployeeDao();
                var encryptMd5Password = Encryptor.MD5Hash(employee.Password);
                employee.Password = encryptMd5Password;
                long id = dao.Insert(employee);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("", "Add employee successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var dao = new EmployeeDao();
                if (!string.IsNullOrEmpty(employee.Password))
                {
                    var encryptMd5Password = Encryptor.MD5Hash(employee.Password);
                    employee.Password = encryptMd5Password;
                }
                var result = dao.Update(employee);
                if (result)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("", "Modify employee successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new EmployeeDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}