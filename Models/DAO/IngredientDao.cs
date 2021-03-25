using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class IngredientDao
    {
        static CoffeeShopDbContext db = null;
        public IngredientDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Ingredient entity)
        {
            db.Ingredients.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public IEnumerable<Ingredient> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Ingredient> model = db.Ingredients;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }
        public Ingredient ViewDetail(int id)
        {
            return db.Ingredients.Find(id);
        }
        public bool Update(Ingredient entity)
        {
            try
            {
                var item = db.Ingredients.Find(entity.Id);
                item.Id = entity.Id;
                item.Name = entity.Name;
                item.Quantity = entity.Quantity;
                item.Unit = entity.Unit;
                item.CreatedDate = item.CreatedDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }
        public static bool UpdateQuantity(int id)
        {
            try
            {
                var item = db.Ingredients.Find(id);
                item.Quantity -= 2;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }
        public static bool MultiUpdateQuantity(int id, int timesOfTheRequest)
        {
            try
            {
                var item = db.Ingredients.Find(id);
                item.Quantity -= (2*timesOfTheRequest);
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
                var item = db.Ingredients.Find(id);
                db.Ingredients.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool AddQuantity(int id)
        {
            try
            {
                var item = db.Ingredients.Find(id);
                item.Quantity += 100;
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
