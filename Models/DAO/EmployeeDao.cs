using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class EmployeeDao
    {
        CoffeeShopDbContext db = null;
        public EmployeeDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Employee entity)
        {
            db.Employees.Add(entity);
            db.SaveChanges();
            return entity.EmployeeID;
        }
        public IEnumerable<Employee> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Employee> model = db.Employees;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Email.Contains(searchString) || x.EmployeeName.Contains(searchString));
            }
            return model.OrderBy(x => x.EmployeeID).ToPagedList(page, pageSize);
        }
        public Employee GetByEmail(string Email)
        {
            return db.Employees.SingleOrDefault(x => x.Email == Email);
        }

        public Employee GetByName(string name)
        {
            return db.Employees.SingleOrDefault(x => x.EmployeeName == name);
        }
        public Employee ViewDetail(int id)
        {
            return db.Employees.Find(id);
        }
        public bool Update(Employee entity)
        {
            try
            {
                var employee = db.Employees.Find(entity.EmployeeID);
                employee.EmployeeID = entity.EmployeeID;
                employee.EmployeeName = entity.EmployeeName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    employee.Password = entity.Password;
                }
                employee.DOB = entity.DOB;
                employee.Email = entity.Email;
                employee.Role = entity.Role;
                employee.ShopID = entity.ShopID;
                employee.Status = entity.Status;
                employee.Image = entity.Image;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }
        public int Login(string email, string passWord)
        {
            var result = db.Employees.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == 1)
                {
                    if (result.Password == passWord) return 1;
                    else return -2;
                }
                else
                {
                    return -1;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
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
