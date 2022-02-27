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
    }
}
