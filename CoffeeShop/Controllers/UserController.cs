using CoffeeShop.Common;
using CoffeeShop.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

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
                    Session["Id"] = customer.CustomerID;
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Sai email hoặc mật khẩu!");
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(string id)
        {
            var customer = new CustomerDao().ViewDetail(int.Parse(id));
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var rawCustomer = dao.ViewDetail(customer.CustomerID);
                customer.CustomerID = rawCustomer.CustomerID;
                customer.Password = rawCustomer.Password;
                customer.CreatedDate = rawCustomer.CreatedDate;
                bool result = dao.Update(customer);
                if (result)
                {
                    Session["Success"] = "Sửa thành công!";
                    return RedirectToAction("Edit", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Customer successfully");
                }
            }
            return View("Index");
        }
        public ActionResult ViewHistory(int id)
        {
            var dao = new OrderDetailDao();
            var model = dao.ListAllHistory(id, 1, 10);
            return View(model);
        }
    }
}