﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessLayer;
using ServiceModel;
using System.Net;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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

        /// <summary>
        /// Get the details of an account
        /// </summary>
        /// <param name="accountId">The account number</param>
        /// <returns>Returns the detail of the account</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Get(int accountId)
        {
            if (!int.TryParse(accountId.ToString(), out _))
                return BadRequest("Enter a valid account id");

            var response = await _accountProcess.Get(accountId);

            return response != null ? Ok(response) : NotFound(response);
        }
    }
}
