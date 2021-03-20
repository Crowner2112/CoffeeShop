using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class OrderDao
    {
        CoffeeShopDbContext db = null;
        public OrderDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.OrderID;
        }
        public IEnumerable<Order> ListAllPaging(string searchNumber, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchNumber))
            {
                model = model.Where(x => x.OrderID == int.Parse(searchNumber));
            }
            return model.OrderBy(x => x.OrderID).ToPagedList(page, pageSize);
        }
        public Order ViewDetail(int id)
        {
            return db.Orders.Find(id);
        }
        public bool Update(Order entity)
        {
            try
            {
                var Order = db.Orders.Find(entity.OrderID);
                Order.OrderID = entity.OrderID;
                Order.CustomerID = entity.CustomerID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var Order = db.Orders.Find(id);
                db.Orders.Remove(Order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
