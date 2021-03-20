using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class BillDetailDao
    {
        CoffeeShopDbContext db = null;
        public BillDetailDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(BillDetail entity)
        {
            db.BillDetails.Add(entity);
            db.SaveChanges();
            return entity.BillID;
        }
        public IEnumerable<BillDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<BillDetail> model = db.BillDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.BillID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.BillDetailID).ToPagedList(page, pageSize);
        }
        public IEnumerable<BillDetail> ListAllPaging(int billId, string searchString, int page, int pageSize)
        {
            IQueryable<BillDetail> model = db.BillDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.BillID == int.Parse(searchString));
            }
            return model.Where(x => x.BillID == billId).OrderBy(x => x.BillID).ToPagedList(page, pageSize);
        }
        public BillDetail ViewDetail(int id)
        {
            return db.BillDetails.Find(id);
        }
        public bool Update(BillDetail entity)
        {
            try
            {
                var BillDetail = db.BillDetails.Find(entity.BillDetailID);
                BillDetail.BillDetailID = entity.BillDetailID;
                BillDetail.BillID = entity.BillID;
                BillDetail.ProductID = entity.ProductID;
                BillDetail.Quantity = entity.Quantity;
                BillDetail.Warranty = entity.Warranty;
                BillDetail.Total = entity.Total;
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
                var BillDetail = db.BillDetails.Find(id);
                db.BillDetails.Remove(BillDetail);
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
