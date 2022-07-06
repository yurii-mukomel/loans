using BLL.DTO;
using BLL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Helpers
{
    public class JwtGenerateHelper
    {
        private readonly IConfiguration _configuration;

        public JwtGenerateHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(AuthenticateRequest authenticateRequest, UserDTO userDTO)
        {
            var identity = GetIdentity(authenticateRequest, userDTO);

            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    notBefore: now,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.AddHours(7),
                    signingCredentials: signIn);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(AuthenticateRequest authenticateRequest, UserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.NameId, userDTO.Id.ToString()),
            };
            ClaimsIdentity claimsIdentity =
            new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
