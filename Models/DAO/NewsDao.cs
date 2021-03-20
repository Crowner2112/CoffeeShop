using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class NewsDao
    {
        CoffeeShopDbContext db = null;
        public NewsDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(News entity)
        {
            db.News.Add(entity);
            db.SaveChanges();
            return entity.NewsID;
        }
        public List<News> ListAllPosts()
        {
            return db.News.ToList();
        }
        public IEnumerable<News> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<News> model = db.News;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NewsID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.NewsID).ToPagedList(page, pageSize);
        }
        public News ViewDetail(int id)
        {
            return db.News.Find(id);
        }
        public bool Update(News entity)
        {
            try
            {
                var News = db.News.Find(entity.NewsID);
                News.NewsID = entity.NewsID;
                News.Title = entity.Title;
                News.Discription = entity.Discription;
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
                var News = db.News.Find(id);
                db.News.Remove(News);
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
