using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace RSEvents.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetToken")]
        [Authorize(AuthenticationSchemes = "Discord")]
        public object GetToken()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string key = _configuration.GetValue<String>("Jwt:SigningKey");
            string issuer = _configuration.GetValue<String>("Jwt:Issuer");
            string audience = _configuration.GetValue<String>("Jwt:Audience");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("discordId", userId));

            var token = new JwtSecurityToken(
                issuer,
                audience,
                permClaims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: credentials
            );

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new
            {
                Apitoken = jwt_token
            };
        }
    }
}