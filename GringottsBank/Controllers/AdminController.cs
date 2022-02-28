using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GringottsBank.Controllers
{
    /// <summary>
    /// Admin endpoints help to generate bearer token for authorization
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        protected readonly string _secretKey;
        public AdminController()
        {
            _secretKey = "getir";
        }

        /// <summary>
        /// Generate the bearer token to use endpoints
        /// </summary>
        /// <param name="secretKey">The secret key is `getir`</param>
        /// <returns>Returns the bearer token for authorization</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GenerateToken(string secretKey)
        {
            if (!this._secretKey.Equals(secretKey, StringComparison.InvariantCultureIgnoreCase))
                return new UnauthorizedObjectResult("Invalid Secret Key!!!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var token = new JwtSecurityToken("https://gringottsbankapi.azurewebsites.net/", "https://gringottsbankapi.azurewebsites.net/", expires: DateTime.Now.AddDays(30), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new OkObjectResult($"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}");
        }
    }
}
