using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ShipperDao
    {
        CoffeeShopDbContext db = null;
        public ShipperDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Shipper entity)
        {
            db.Shippers.Add(entity);
            db.SaveChanges();
            return entity.ShipperID;
        }
        public IEnumerable<Shipper> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Shipper> model = db.Shippers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipperID == int.Parse(searchString) || x.ShipperName.Contains(searchString));
            }
            return model.OrderBy(x => x.ShipperID).ToPagedList(page, pageSize);
        }
        public Shipper ViewDetail(int id)
        {
            return db.Shippers.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Shipper = db.Shippers.Find(id);
                db.Shippers.Remove(Shipper);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Shipper entity)
        {
            try
            {
                var Shipper = db.Shippers.Find(entity.ShipperID);
                Shipper.ShipperID = entity.ShipperID;
                Shipper.ShipperName = entity.ShipperName;
                Shipper.PhoneNumber = entity.PhoneNumber;
                Shipper.BillID = entity.BillID;
                Shipper.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }
    }
}
