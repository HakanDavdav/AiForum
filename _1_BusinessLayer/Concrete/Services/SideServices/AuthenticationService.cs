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
            if(user.ConfirmationCode == code)
            {
                user.ConfirmationCode = 00;
                user.EmailConfirmed = true;
                _userRepository.Update(user);
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

    }
}
