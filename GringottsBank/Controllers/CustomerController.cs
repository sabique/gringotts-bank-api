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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProcess _customerProcess;
        public CustomerController(ICustomerProcess customerProcess)
        {
            this._customerProcess = customerProcess;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _customerProcess.Add(customer);

            return response.StatusCode == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        } 
    }
}
