using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VSCodeWebAPIJwtSecurity.Models;

namespace VSCodeWebAPIJwtSecurity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Email == "a@a.com" && model.Password == "pwd")
            {
                return Ok(new { Success = true, token = GenerateToken(model.Email) });
            }
            return Ok(new { Success = false, Message = "Invalid email/password" });
        }

        private string GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim("email", email),
                new Claim("Jti", Guid.NewGuid().ToString())
            };

            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecudfhsdfhdfhsdfhsdhdfhreKey"));

            var token = new JwtSecurityToken
                (
                issuer: "Jwt",
                audience: "Jwt",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
