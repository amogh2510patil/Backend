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

        public customerController(customerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        [HttpGet]
        [Route("Getcustomer")]
        public async Task<IEnumerable<customer>> Getcustomer()
        {
            return await _customerDbContext.customer.ToListAsync();

        }

        [HttpPost]
        [Route("Addcustomer")]
        public async Task<customer> Addstudent(customer objcustomer)
        {
            _customerDbContext.customer.Add(objcustomer);
            await _customerDbContext.SaveChangesAsync();
            return objcustomer;
        }

        [HttpPatch]
        [Route("Updatecustomer/{accountnum}")]
        public async Task<customer> Updatecustomer(customer objcustomer)
        {
            _customerDbContext.Entry(objcustomer).State = EntityState.Modified;
            await _customerDbContext.SaveChangesAsync();
            return objcustomer;
        }

        [HttpDelete]
        [Route("Deletecustomer/{accountnum}")]
        public bool Deletecustomer(int accountnum)
        {
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
