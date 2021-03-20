using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class EventDao
    {
        CoffeeShopDbContext db = null;
        public EventDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Event entity)
        {
            db.Events.Add(entity);
            db.SaveChanges();
            return entity.EventID;
        }
        public IEnumerable<Event> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Event> model = db.Events;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.EventName.Contains(searchString) || x.EventID == int.Parse(searchString));
            }
            return model.OrderBy(x => x.EventID).ToPagedList(page, pageSize);
        }
        public Event GetByName(string name)
        {
            return db.Events.SingleOrDefault(x => x.EventName == name);
        }
        public Event ViewDetail(int id)
        {
            return db.Events.Find(id);
        }
        public bool Update(Event entity)
        {
            try
            {
                var Event = db.Events.Find(entity.EventID);
                Event.EventID = entity.EventID;
                Event.EventName = entity.EventName;
                Event.EventCode = entity.EventCode;
                Event.Discription = entity.Discription;
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
                var Event = db.Events.Find(id);
                db.Events.Remove(Event);
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
