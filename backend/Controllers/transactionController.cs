using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class transactionController : Controller
    {
            private readonly customerDbContext _transactionDbContext;
            private transactionRepo _transactionRepo; // = new transactionRepo(_transactionDbContext);


            public transactionController(customerDbContext transactionDbContext)
            {
                _transactionDbContext = transactionDbContext;
                _transactionRepo = new transactionRepo(new customerDbContext());
            }

        

            [HttpGet]
            [Route("Gettransaction/{accNo}")]
            public async Task<IEnumerable<transaction>> Gettransaction(int accNo)
            {

               //  return cus;
                List<transaction> transactions= _transactionRepo.Gettransaction(accNo);
                return transactions;

            }

            [HttpPost]
            [Route("Addtransaction")]
            public async Task<transaction> Addstudent(transaction objtransaction)
            {
            //_transactionDbContext.transaction.Add(objtransaction);
            //await _transactionDbContext.SaveChangesAsync();
            _transactionRepo.Inserttransaction(objtransaction);
                return objtransaction;
            }

            [HttpPatch]
            [Route("Updatetransaction/{transactionnum}")]
            public async Task<transaction> Updatetransaction(transaction objtransaction)
            {
                _transactionRepo.Updatetransaction(objtransaction);
                return objtransaction;
                //_transactionDbContext.Entry(objtransaction).State = EntityState.Modified;
                //await _transactionDbContext.SaveChangesAsync();
                //return objtransaction;
            }

            [HttpDelete]
            [Route("Deletetransaction/{transactionnum}")]
            public bool Deletetransaction(int transactionnum)
            {
                return _transactionRepo.Deletetransaction(transactionnum);

                
            }
        }

}
