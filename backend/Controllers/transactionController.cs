using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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


        [HttpGet]
        [Route("GetStatement/{accNo}/{startDate}/{endDate}")]
        public async Task<IEnumerable<transaction>> GetStatement(int accNo,DateTime startDate, DateTime endDate)
        {

            //  return cus;
            List<transaction> transactions = _transactionRepo.GetStatement(accNo,startDate,endDate);
            return transactions;

        }

        [HttpPost]
            [Route("Addtransaction")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> Addtransaction(transaction objtransaction)
            {
            //_transactionDbContext.transaction.Add(objtransaction);
            //await _transactionDbContext.SaveChangesAsync();
            int validity = _transactionRepo.Inserttransaction(objtransaction);
            if (validity == 0)
            {
                return BadRequest("Invalid Customer");
            }

             return Ok(objtransaction);
            }
/*
        [HttpPost]
        [Route("FundTransfer")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FundTransfer(transaction objtransaction)
        {
            //_transactionDbContext.transaction.Add(objtransaction);
            //await _transactionDbContext.SaveChangesAsync();
            int validity = _transactionRepo.FundTransfer(objtransaction);
            if (validity == 0)
            {
                return BadRequest();
            }

            return Ok(objtransaction);
        }*/

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
