using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace _1_BusinessLayer.Concrete.Services.SideServices
{
    public class AuthenticationManager : AbstractAuthenticationManager
    {
        public AuthenticationManager(AbstractUserRepository userRepository) : base(userRepository)
        {
        }

        public override async Task<IdentityResult> ValidateEmailAsync(User user, int code)
        {
            if(user.ConfirmationCode == code)
            {
                user.ConfirmationCode = 00;
                user.EmailConfirmed = true;
                await _userRepository.UpdateAsync(user);
                return IdentityResult.Success;

            }
            else
            {
                user.EmailConfirmed= false;
                return IdentityResult.Failed(new ValidationError("Confirmation code is wrong!"));
            }              
        }


        public override IdentityResult CheckEmailValidation(User user)
        {
            if (user.EmailConfirmed == true)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new ValidationError("Your email is not confirmed!"));
            }
        }

    }
}
