using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace _1_BusinessLayer.Concrete.Services.SideServices
{
    public class AuthenticationService : AbstractAuthenticationService
    {
        public AuthenticationService(AbstractUserRepository userRepository) : base(userRepository)
        {
        }

        public override bool CheckMail(User user, int code)
        {
            if(user.confirmationCode == code)
            {
                user.confirmationCode = 00;
                user.EmailConfirmed = true;
                return true;
            }
            else
            {
                user.EmailConfirmed= false;
                return false;
            }              
        }


        public override bool CheckMail(User user)
        {
            if (user.EmailConfirmed == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes("SecretKey");
            List<Claim> jwtUser = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),

            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtUser),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
