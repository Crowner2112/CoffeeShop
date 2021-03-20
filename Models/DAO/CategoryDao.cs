using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class CategoryDao
    {
        CoffeeShopDbContext db = null;
        public CategoryDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryID;
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CategoryName.Contains(searchString));
            }
            return model.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize);
        }
        public Category GetById(int id)
        {
            return db.Categories.SingleOrDefault(x => x.CategoryID == id);
        }
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }
        public bool Update(Category entity)
        {
            try
            {
                var Category = db.Categories.Find(entity.CategoryID);
                Category.CategoryID = entity.CategoryID;
                Category.CategoryName = entity.CategoryName;
                Category.Status = entity.Status;
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
                var Category = db.Categories.Find(id);
                db.Categories.Remove(Category);
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
