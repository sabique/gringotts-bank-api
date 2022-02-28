using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessLayer;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ITransactionDetailProcess _transactionProcess;

        public TransactionController(TransactionProcessResolver processAccessor, ITransactionDetailProcess transactionProcess)
        {
            this._depositProcess = processAccessor(Utility.Enum.TransactionType.Deposit);
            this._withdrawProcess = processAccessor(Utility.Enum.TransactionType.Withdraw);
            this._transactionProcess = transactionProcess;
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

        /// <summary>
        /// Get all the transactions of an account
        /// </summary>
        /// <param name="accountId">The account number of customer</param>
        /// <param name="skip">Skip the number of transation from the top, default is 0</param>
        /// <param name="take">Return number of transaction for a account, default is 10</param>
        /// <returns>Returns the list of transactions for an account</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions(int accountId, int skip = 0, int take = 10)
        {
            if (!int.TryParse(accountId.ToString(), out _))
                return BadRequest("Enter a valid account id");

            var response = await _transactionProcess.Transactions(accountId, skip, take);

            return (response != null)? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Get all the transactions of an account between a time period
        /// </summary>
        /// <param name="accountId">The account number of customer</param>
        /// <param name="startDate">The transactions occurred on or after this date</param>
        /// <param name="endDate">The transactions occurred before this date</param>
        /// <param name="skip">Skip the number of transation from the top, default is 0</param>
        /// <param name="take">Return number of transaction for a account, default is 10</param>
        /// <returns>Returns the list of transactions for an account</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsBetweenDate(int accountId, string startDate, string endDate, int skip = 0, int take = 10)
        {
            #region Validations
            if (!int.TryParse(accountId.ToString(), out int id) || id <= 0)
                return BadRequest("Enter a valid account id");

            if (!IsValidDate(startDate, out var startDateTime))
                return BadRequest("Start date is invalid. Please provide a correct date in valid yyyy-MM-dd format.");

            if (!IsValidDate(endDate, out var endDateTime))
                return BadRequest("End date is invalid. Please provide a correct date in valid yyyy-MM-dd format.");

            if (startDateTime >= endDateTime)
                return BadRequest("Start date cannot be greater or equal to end date."); 
            #endregion

            var response = await _transactionProcess.Transactions(accountId, startDateTime, endDateTime, skip, take);

            return (response != null) ? Ok(response) : NotFound(response);
        }

        private bool IsValidDate(string date, out DateTime dateTime)
        {
            dateTime = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(date)
                && !DateTime.TryParseExact(date, "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
            {
                return false;
            }

            return true;
        }

    }
}
