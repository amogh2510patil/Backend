using Microsoft.EntityFrameworkCore;

namespace backend.Models
{

        interface ICustomer
        {
            customer GetCustomer(int accNo);
            List<customer> GetAll();
            bool DeleteCustomer(int accNo);
            int InsertCustomer(customer customer);
            int UpdateCustomer(customer customer);
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
            //using (var db = new customerDbContext())
            {

                db.customer.Add(customer);

                db.SaveChangesAsync();
                return 1;
            }

        }

        public int UpdateCustomer(customer customerup)
        {
            //using (var db = new customerDbContext())
            {
                db.Entry(customerup).State = EntityState.Modified;
                db.SaveChangesAsync();
                return 1;
            }
        }
    }
}
