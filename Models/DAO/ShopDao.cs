using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ShopDao
    {
        CoffeeShopDbContext db = null;
        public ShopDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Shop entity)
        {
            db.Shops.Add(entity);
            db.SaveChanges();
            return entity.ShopID;
        }
        public IEnumerable<Shop> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Shop> model = db.Shops;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Address.Contains(searchString) || x.ShopID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.ShopID).ToPagedList(page, pageSize);
        }
        public Shop GetByAddress(string address)
        {
            return db.Shops.SingleOrDefault(x => x.Address == address);
        }
        public Shop ViewDetail(int id)
        {
            return db.Shops.Find(id);
        }
        public bool Update(Shop entity)
        {
            try
            {
                var Shop = db.Shops.Find(entity.ShopID);
                Shop.ShopID = entity.ShopID;
                Shop.Address = entity.Address;
                Shop.Discription = entity.Discription;
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
                var Shop = db.Shops.Find(id);
                db.Shops.Remove(Shop);
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
