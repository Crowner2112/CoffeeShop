using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ProductDao
    {
        public static CoffeeShopDbContext db = null;
        public ProductDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ProductID;
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProductName == searchString);
            }
            return model.OrderBy(x => x.ProductID).ToPagedList(page, pageSize);
        }
        public Product GetById(int id)
        {
            return db.Products.SingleOrDefault(x => x.ProductID == id);
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
        public List<Product> ListAllProduct()
        {
            return db.Products.ToList();
        }
        public bool Update(Product entity)
        {
            try
            {
                var Product = db.Products.Find(entity.ProductID);
                Product.ProductID = entity.ProductID;
                Product.ProductName = entity.ProductName;
                Product.CategoryID = entity.CategoryID;
                Product.Price = entity.Price;
                Product.PromotionPrice = entity.PromotionPrice;
                Product.Warranty = entity.Warranty;
                Product.Status = entity.Status;
                Product.Image = entity.Image;
                Product.Discription = entity.Discription;
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
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void UpdateViewCount(int id)
        {
            var product = db.Products.Find(id);
            if (product.ViewCount == 0)
            {
                product.ViewCount = 1;
            }
            else
            {
                product.ViewCount++;
            }
            db.SaveChanges();
        }
    }
}
