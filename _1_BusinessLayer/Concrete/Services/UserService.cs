using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.Errors;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace _1_BusinessLayer.Concrete.Services_Tools
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository,
            UserManager<User> userManager, SignInManager<User> signInManager)
            : base(tokenSender, userRepository, userManager, signInManager)
        {
        }

        public override async Task<IdentityResult> ActivateTwoFactorAuthentication(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, true);
                return twoFactorResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DisableTwoFactorAuthentication(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user,true);
                return twoFactorResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ChangeEmail(int userId,string newEmail ,string changeEmailToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var changeEmailResult = await _userManager.ChangeEmailAsync(user, newEmail, changeEmailToken);
                return changeEmailResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return changePasswordResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ConfirmEmail(UserLoginDto userLoginDto, string emailConfirmationToken)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginDto.EmailOrUsernameOrPhoneNumber) ??
                       await _userRepository.GetByUsernameAsync(userLoginDto.EmailOrUsernameOrPhoneNumber);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    var confirEmailResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
                    return confirEmailResult;
                }
                return IdentityResult.Failed(new UnexpectedError("Email is already confirmed"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
               
        }

        public override async Task<IdentityResult> ConfirmPhoneNumber(int userId, string twoFactorToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.PhoneNumber != null)
                {
                    var confirmPhoneNumberResult = await _userManager.VerifyChangePhoneNumberTokenAsync(user, twoFactorToken, user.PhoneNumber);
                    if (confirmPhoneNumberResult == true)
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("Invalid token"));
                }
                return IdentityResult.Failed(new NotFoundError("User phone number not found"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }


        public override async Task<IdentityResult> Login(UserLoginDto userLoginDto, string twoFactorToken)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginDto.EmailOrUsernameOrPhoneNumber) ??
                       await _userRepository.GetByUsernameAsync(userLoginDto.EmailOrUsernameOrPhoneNumber);
            if (user != null)
            {
                if (await _signInManager.CanSignInAsync(user))
                {
                    if(await _signInManager.IsTwoFactorEnabledAsync(user))
                    {
                        var signInTwoFactorResult = await _signInManager.TwoFactorAuthenticatorSignInAsync(twoFactorToken,false,false);
                        return signInTwoFactorResult.ToIdentityResult();
                    }
                    var signInResult = await _signInManager.PasswordSignInAsync(user,userLoginDto.Password,false,false);
                    return signInResult.ToIdentityResult();
                }
                try { await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user); } 
                catch(Exception ex) { return IdentityResult.Failed(new UnexpectedError("Email connection error")); throw; };
                return IdentityResult.Failed(new UnauthorizedError("Account is not confirmed. Your confirmation code has sent"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;
        }
      

        public override async Task<IdentityResult> PasswordReset(int userId, string newPassword ,string resetPasswordToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user != null)
            {
                var passwordResetResult = _userManager.ResetPasswordAsync(user,resetPasswordToken,newPassword);
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.UserRegisterToUser();
            if (user != null)
            {
                var createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);
                try { await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user); } 
                catch (Exception ex) { return IdentityResult.Failed(new UnexpectedError("Email connection error")); throw; }
                return createUserResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> EditPreferences(int userId, UserPreferencesDto userPreferencesDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                //
            }
            return  IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var updatedUser = userEditProfileDto.Update_UserEditProfileDtoToUser(user);
                await _userRepository.UpdateAsync(updatedUser);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }

        public override async Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.UserName==oldUsername)
                {
                    user.UserName = newUsername;
                    await _userRepository.UpdateAsync(user);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("Wrong username"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }
    }
}

      