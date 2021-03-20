using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class OrderDetailDao
    {
        CoffeeShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(OrderDetail entity)
        {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
            return entity.OrderDetailID;
        }
        public IEnumerable<OrderDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.OrderDetailID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.OrderDetailID).ToPagedList(page, pageSize);
        }
        public IEnumerable<OrderDetail> ListAllPaging(int orderId, string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.OrderID == int.Parse(searchString));
            }
            return model.Where(x => x.OrderID == orderId).OrderBy(x => x.OrderID).ToPagedList(page, pageSize);
        }
        public OrderDetail ViewDetail(int id)
        {
            return db.OrderDetails.Find(id);
        }
        public bool Update(OrderDetail entity)
        {
            try
            {
                var OrderDetail = db.OrderDetails.Find(entity.OrderDetailID);
                OrderDetail.OrderDetailID = entity.OrderDetailID;
                OrderDetail.OrderID = entity.OrderID;
                OrderDetail.ProductID = entity.ProductID;
                OrderDetail.Quantity = entity.Quantity;
                OrderDetail.EventCode = entity.EventCode;
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
                var OrderDetail = db.OrderDetails.SingleOrDefault(x => x.OrderDetailID == id);
                db.OrderDetails.Remove(OrderDetail);
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
