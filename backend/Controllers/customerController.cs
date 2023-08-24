using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize]
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
        [Route("GetAll"),Authorize(Roles="Admin")]
        public async Task<IEnumerable<customer>> GetAll()
        {

            return _customerRepo.GetAll();

        }

        [HttpGet]
        [Route("Getcustomer/{accNo}")]
        [AllowAnonymous]
        public customer Getcustomer(int accNo)
        {

            
            return _customerRepo.GetCustomer(accNo);

        }

        [HttpPost]
        [Route("Addcustomer")]
        [AllowAnonymous]
        public async Task<customer> Addstudent(customer objcustomer)
        {
            //_customerDbContext.customer.Add(objcustomer);
            //await _customerDbContext.SaveChangesAsync();
            _customerRepo.InsertCustomer(objcustomer);
            return objcustomer;
        }

        [HttpPatch]

        [Route("SetPin/{accNo}/{pin}")]
        public bool SetPin(int accNo, int pin)
        {
            return _customerRepo.SetPin(accNo, pin);
        }

        [HttpPatch]
        [Route("Updatecustomer/{accountnum}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public bool Deletecustomer(int accountnum)
        {
            return _customerRepo.DeleteCustomer(accountnum);
        }
    }
}
