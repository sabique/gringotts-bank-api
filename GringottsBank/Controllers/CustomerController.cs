using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessLayer;
using ServiceModel;
using System.Net;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    /// <summary>
    /// Manage the operations for customer entity
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProcess _customerProcess;
        public CustomerController(ICustomerProcess customerProcess)
        {
            this._customerProcess = customerProcess;
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customer">The details of the new customer</param>
        /// <returns>If success, then returns the response with customer id else bad request</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _customerProcess.Add(customer);

            return response.StatusCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Get the details of an existing customer
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        /// <returns>If found, then returns the details of the customer else empty response</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Get(int customerId)
        {
            if (!int.TryParse(customerId.ToString(), out _))
                return BadRequest("Enter a valid customer id");

            var response = await _customerProcess.Get(customerId);

            return response != null ? Ok(response) : NotFound(response);
        }
    }
}
