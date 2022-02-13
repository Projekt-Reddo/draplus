using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DraplusApi.Models;

namespace DraplusApi.Helper
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string key = "this is my custom Secret key for authnetication";
        public JwtGenerator(string key)
        {
            this.key = key;
        }
        public Claim[] GenerateClaims(User user){
            var claims = new[]
                {
                    // new Claim(JwtRegisteredClaimNames.Sub, "ABC"),
                    // new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    // new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    // new Claim("Avatar", user.Avatar),
                    // new Claim(JwtRegisteredClaimNames.Iss, user.Issuer),
                    // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };
            return claims;
        }
        public string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            // Declare token and properties
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature
                )
            };

            // Generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
