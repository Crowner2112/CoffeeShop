using CoffeeShop.Common;
using CoffeeShop.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var result = dao.Login(model.Email, Encryptor.MD5Hash(model.Password));
                if (result == true)
                {
                    var customer = dao.GetByEmail(model.Email);
                    var customerSession = new EmployeeLogin();
                    customerSession.EmployeeName = customer.CustomerName;
                    customerSession.Email = customer.Email;
                    Session.Add(CommonConstants.EMPLOYEE_SESSION, customerSession);
                    Session["Account"] = customer.CustomerName;
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Incorect email or password");
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                if (dao.CheckCustomerName(model.CustomerName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (dao.CheckPN(model.PhoneNumber))
                {
                    ModelState.AddModelError("", "SĐT này đã tồn tại!");
                }
                else
                {
                    var customer = new Customer();
                    customer.DOB = model.DOB;
                    customer.CustomerName = model.CustomerName;
                    customer.Password = Encryptor.MD5Hash(model.Password);
                    customer.PhoneNumber = model.PhoneNumber;
                    customer.Address = model.Address;
                    customer.CreatedDate = DateTime.Now;
                    customer.Email = model.Email;
                    var result = dao.Insert(customer);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công!";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                    }
                }
            }
            return View(model);
        }
    }
}