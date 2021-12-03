using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesainAPI1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesainAPI1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if(user == null)
            {
                return BadRequest("Invalid client request");
            }

            if(user.UserName == "damian" && user.Password == "123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@34"));
                var signingCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredential
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                

                return Ok(new { Token = tokenString });
            }

            return View();
        }
    }
}
