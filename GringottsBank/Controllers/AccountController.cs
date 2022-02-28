using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessLayer;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountProcess _accountProcess;
        public AccountController(IAccountProcess accountProcess)
        {
            this._accountProcess = accountProcess;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add(OpenAccount account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _accountProcess.Add(account);

            return response.StatusCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Get all the accounts of a customer
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="skip">Skip the number of account from the top, default is 0</param>
        /// <param name="take">Return number of account for a customer, default is 10</param>
        /// <returns>Returns the list of accounts of a customer</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerAccounts(int customerId, int skip = 0, int take = 10)
        {
            if (!int.TryParse(customerId.ToString(), out _))
                return BadRequest("Enter a valid customer id");

            var response = await _accountProcess.GetCustomerAccounts(customerId, skip, take);

            return response != null ? Ok(response) : NotFound(response);
        }
    }
}
