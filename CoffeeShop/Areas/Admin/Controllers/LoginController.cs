using CoffeeShop.Areas.Admin.Models;
using CoffeeShop.Common;
using Models.DAO;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new EmployeeDao();
                var result = dao.Login(model.Email, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var employee = dao.GetByEmail(model.Email);
                    var employeeSession = new EmployeeLogin();
                    Session["empname"] = dao.GetByEmail(model.Email).EmployeeName;
                    employeeSession.EmployeeName = employee.EmployeeName;
                    employeeSession.Email = employee.Email;
                    Session.Add(CommonConstants.EMPLOYEE_SESSION, employeeSession);
                    Session["Account"] = employee.EmployeeName;
                    Session["Image"] = employee.Image;
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "This account does not exsits");

                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "This account was locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Incorect email or password");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult GoToShopView()
        {
            Session.Abandon();
            return Redirect("/Home");
        }
    }
}