using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Models
{

        interface ICustomer
        {
            customer GetCustomer(int accNo);
            List<customer> GetAll();
            bool DeleteCustomer(int accNo);
            int InsertCustomer(customer customer);
            customer UpdateCustomer(customer customer);
        }

    public class customerRepo : ICustomer
    {
        private readonly customerDbContext db;
        
        public customerRepo(customerDbContext _customerDbContext) {
            db = _customerDbContext;
        }

        public customer GetCustomer(int accNo)
        {
            //using (var db = new customerDbContext())
            {
                customer customer = db.customer.Find(accNo);
                return customer;
            }
        }

        public List<customer> GetAll()
        {
            // using (var db = new customerDbContext())
            {
                List<customer> customerList = db.customer.ToList();
                return customerList;
            }

        }


        public bool DeleteCustomer(int accNo)
        {
            //using (var db = new customerDbContext())
            {
                bool a = false;
                var customer = db.customer.Find(accNo);
                if (customer != null)
                {
                    a = true;
                    db.Entry(customer).State = EntityState.Deleted;
                    var user = db.User.Where(u => u.accNo == customer.accountnum && u.Role == "User").FirstOrDefault();
                    if (user != null)
                    {
                        db.Entry(user).State = EntityState.Deleted;
                    }

                    db.SaveChanges();

                }
                else
                {
                    a = false;
                }
                return a;
            }
        }

        public int InsertCustomer(customer customer)
        {
            Random rnd=new Random();
            bool uni = true;
            int num = 100000;
            while (uni)
            {
                num = rnd.Next(100000, 999999);
                customer cust = db.customer.Find(num);
                if (cust == null)
                {
                    uni=false;
                }
            }
            
            //using (var db = new customerDbContext())
            {
                customer.accountnum = num;
                db.customer.Add(customer);

                db.SaveChangesAsync();
                return 1;
            }

        }

        public customer UpdateCustomer(customer customerup)
        {
            //using (var db = new customerDbContext())
            {
                db.Entry(customerup).State = EntityState.Modified;
                db.SaveChangesAsync();
                return customerup;
            }
        }

        public bool SetPin(int accNo,int pin)
        {
            var customer=db.customer.Find(accNo);
            if (customer != null)
            {
                customer.pinnum = pin;
                db.Entry(customer).State= EntityState.Modified;
                db.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
