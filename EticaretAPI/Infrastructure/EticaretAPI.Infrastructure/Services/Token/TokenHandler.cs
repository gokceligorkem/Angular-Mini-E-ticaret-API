using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler   
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second,AppUser user)
        {
            Application.DTOs.Token token = new();
            //Securitykeyin simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Experition = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Experition,
                notBefore:DateTime.UtcNow,
                signingCredentials:signingCredentials,
                claims:new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );
            //Token oluşturucu  sınıfından örnek alalım
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
          
            token.RefreshToken = CreateRefrestToken();
            return token;
        }

        public string CreateRefrestToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
