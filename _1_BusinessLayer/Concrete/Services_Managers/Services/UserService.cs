using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Errors;
using _1_BusinessLayer.Concrete.Mappers;
using _1_BusinessLayer.Concrete.Services.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class UserService : AbstractUserService 
    {
        public UserService(AbstractAuthenticationManager authenticationManager,
            AbstractMailManager mailManager, AbstractUserRepository userRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager) : base(authenticationManager, mailManager, userRepository, userManager, signInManager)
        {
        }

        public override Task<IdentityResult> ChangeEmailAsync(int id, string email)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> ChangePasswordAsync(int id,string password)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> ChangeUserPreferencesAsync(int id, UserPreferences userPreferences)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> ConfirmEmailAsync(int code, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                var result = await _authenticationManager.ValidateEmailAsync(user, id);
                if (result.Succeeded)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new ValidationError("Confirmation code is wrong"));
                }
            }

        }

        public override async Task<IdentityResult> DeleteProfileAsync(int id)
        {
            throw new NotImplementedException();

        }

        public override async Task<IdentityResult> EditProfileAsync(int id, UserProfileDto userProfileDto)
        {
            throw new NotImplementedException();

        }

        public override async Task<IdentityResult> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();

        }

        public override async Task<IdentityResult> LoginAsync(UserLoginDto userLogged)
        {
            var user = await _userRepository.GetByEmailAsync(userLogged.EmailOrUsername)??
                       await _userRepository.GetByUsernameAsync(userLogged.EmailOrUsername);
            if (user == null)
            {
                return IdentityResult.Failed(new NotFoundError("Invalid username or password"));
            }
            else
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user,userLogged.Password,false,false);
                if (signInResult.Succeeded&&_authenticationManager.CheckEmailValidation(user).Succeeded)
                {
                    //SignInResult Mapped to IdentityResult object
                    return signInResult.ToIdentityResult();
                }
                else if(signInResult.Succeeded) 
                {
                    //Passing _authenticationManager's errors
                    return IdentityResult.Failed(_authenticationManager.CheckEmailValidation(user).Errors.ToArray());
                }
                else
                {
                    return signInResult.ToIdentityResult();
                }

            }

        }

        public override async Task<IdentityResult> LogoutAsync(int id)
        {
            throw new NotImplementedException();

        }

        
        public override async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegistered )
        {
            var user = userRegistered.UserRegisterToUser();
            var result = await _userManager.CreateAsync(user,userRegistered.Password);
            if (result.Succeeded)
            {
                await _mailManager.CreateMailConfirmationCodeAsync(user);
                await _userManager.SetTwoFactorEnabledAsync(user, true);

            }
            return result;

        }

        public override Task<IdentityResult> SearchUserAsync(string name)
        {
            throw new NotImplementedException();
        }




    }
}
