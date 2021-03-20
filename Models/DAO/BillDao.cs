using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class BillDao
    {
        CoffeeShopDbContext db = null;
        public BillDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Bill entity)
        {
            db.Bills.Add(entity);
            db.SaveChanges();
            return entity.BillID;
        }
        public IEnumerable<Bill> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Bill> model = db.Bills;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.BillID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.BillID).ToPagedList(page, pageSize);
        }
        public Bill ViewDetail(int id)
        {
            return db.Bills.Find(id);
        }
        public bool Update(Bill entity)
        {
            try
            {
                var Bill = db.Bills.Find(entity.BillID);
                Bill.BillID = entity.BillID;
                Bill.EmployeeID = entity.EmployeeID;
                Bill.ShopID = entity.ShopID;
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
                var Bill = db.Bills.Find(id);
                db.Bills.Remove(Bill);
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
