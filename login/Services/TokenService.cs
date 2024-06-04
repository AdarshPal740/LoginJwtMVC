using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtAuthMvcApp.Models;
using login.Models;

namespace JwtAuthMvcApp.Services
{
    public class TokenService
    {
        private IConfiguration _config;
        public TokenService(IConfiguration config)
        {

            _config = config;

        }

        public string GenerateToken(Logintb user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Email",user.Email),
                

            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["jwt:Issuer"], _config["jwt:Audience"], claims: claims, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
