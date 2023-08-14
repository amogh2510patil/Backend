using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController : ControllerBase
    {
        private readonly customerDbContext _customerDbContext;
        private customerRepo _customerRepo; // = new customerRepo(_customerDbContext);
        

        public customerController(customerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
            _customerRepo = new customerRepo(new customerDbContext());  
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<customer>> GetAll()
        {

            return _customerRepo.GetAll();

        }

        [HttpGet]
        [Route("Getcustomer/{accNo}")]
        public customer Getcustomer(int accNo)
        {

            var cus = _customerDbContext.customer.Find(accNo);
            
            
            return cus;
            //return _customerRepo.GetCustomer(accNo);

        }

        [HttpPost]
        [Route("Addcustomer")]
        public async Task<customer> Addstudent(customer objcustomer)
        {
            //_customerDbContext.customer.Add(objcustomer);
            //await _customerDbContext.SaveChangesAsync();
            _customerRepo.InsertCustomer(objcustomer);
            return objcustomer;
        }

        [HttpPatch]
        [Route("Updatecustomer/{accountnum}")]
        public async Task<customer> Updatecustomer(customer objcustomer)
        {
            _customerRepo.UpdateCustomer(objcustomer);
            return objcustomer;
            //_customerDbContext.Entry(objcustomer).State = EntityState.Modified;
            //await _customerDbContext.SaveChangesAsync();
            //return objcustomer;
        }

        [HttpDelete]
        [Route("Deletecustomer/{accountnum}")]
        public bool Deletecustomer(int accountnum)
        {
            return _customerRepo.DeleteCustomer(accountnum);

            bool a = false;
            var customer = _customerDbContext.customer.Find(accountnum);
            if (customer != null)
            {
                a = true;
                _customerDbContext.Entry(customer).State = EntityState.Deleted;
                _customerDbContext.SaveChanges();

            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
