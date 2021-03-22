using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class CustomerDao
    {
        CoffeeShopDbContext db = null;
        public CustomerDao()
        {
            db = new CoffeeShopDbContext();
        }
        public int Insert(Customer entity)
        {
            db.Customers.Add(entity);
            db.SaveChanges();
            return entity.CustomerID;
        }
        public IEnumerable<Customer> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Customer> model = db.Customers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Email.Contains(searchString) || x.CustomerName.Contains(searchString));
            }
            return model.OrderBy(x => x.CustomerID).ToPagedList(page, pageSize);
        }
        public Customer GetByEmail(string Email)
        {
            return db.Customers.SingleOrDefault(x => x.Email == Email);
        }

        public Customer GetByName(string name)
        {
            return db.Customers.SingleOrDefault(x => x.CustomerName == name);
        }
        public Customer ViewDetail(int id)
        {
            return db.Customers.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Customer = db.Customers.Find(id);
                db.Customers.Remove(Customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckCustomerName(string userName)
        {
            return db.Customers.Count(x => x.CustomerName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }
        public bool CheckPN(string phoneNumber)
        {
            return db.Customers.Count(x => x.PhoneNumber == phoneNumber) > 0;
        }
        public bool Login(string email, string passWord)
        {
            var result = db.Customers.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return false;
            }
            else
            {
                if (result.Password == passWord) return true;
                else return false;
            }
        }

        public bool Update(Customer customer)
        {
            try
            {
                var rawCustomer = db.Customers.Find(customer.CustomerID);
                rawCustomer.CustomerID = customer.CustomerID;
                rawCustomer.CustomerName = customer.CustomerName;
                rawCustomer.Password = customer.Password;
                rawCustomer.DOB = customer.DOB;
                rawCustomer.Email = customer.Email;
                rawCustomer.Address = customer.Address;
                rawCustomer.PhoneNumber = customer.PhoneNumber;
                rawCustomer.CreatedDate = customer.CreatedDate;
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
