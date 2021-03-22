using CoffeeShop.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            ProductDao.UpdateViewCount(id);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ProductID == id))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == id)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = 1;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = 1;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateQuantity(FormCollection form)
        {
            var cart = Session[CartSession];
            int idProduct = int.Parse(form["IDProduct"]);
            int quantity = int.Parse(form["quantity"]);
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                var item = list.Find(x => x.Product.ProductID == idProduct);
                if (item != null)
                {
                    item.Quantity = quantity;
                }
                Session[CartSession] = list;
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.CustomerID = null;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ProductID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return Redirect("/Cart/Success");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}