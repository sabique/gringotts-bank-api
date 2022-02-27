using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessLayer;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static GringottsBank.Startup;

namespace GringottsBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionProcess _depositProcess;
        private readonly ITransactionProcess _withdrawProcess;
        public TransactionController(TransactionProcessResolver processAccessor)
        {
            this._depositProcess = processAccessor(Utility.Enum.TransactionType.Deposit);
            this._withdrawProcess = processAccessor(Utility.Enum.TransactionType.Withdraw);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Deposit(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _depositProcess.Transact(transaction);

            return response.StatusCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Withdraw(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _withdrawProcess.Transact(transaction);

            return response.StatusCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}
