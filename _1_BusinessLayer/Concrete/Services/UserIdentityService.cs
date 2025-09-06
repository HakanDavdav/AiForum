using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;

using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _1_BusinessLayer.Concrete.Tools.Senders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.SmsTypes;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserIdentityService : AbstractUserIdentityService
    {
        public UserIdentityService(AbstractUserRepository userRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager, AbstractUserPreferenceRepository userPreferenceRepository, 
            GeneralSender generalSender) : base(userRepository, userManager, signInManager, userPreferenceRepository, generalSender)
        {
        }

        public override async Task<IdentityResult> ActivateTwoFactorAuthentication(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, true);
                Console.WriteLine(user.TwoFactorEnabled);
                return twoFactorResult;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> DisableTwoFactorAuthentication(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
                return twoFactorResult;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> ConfirmChangeEmailToken(int userId, string newEmail, string changeEmailToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var changeEmailResult = await _userManager.ChangeEmailAsync(user, newEmail, changeEmailToken);
                return changeEmailResult;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }
        public override async Task<IdentityResult> ConfirmEmailConfirmationToken(string emailConfirmationToken, string EmailOrUsernameOrPassword)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Email == EmailOrUsernameOrPassword)) ?? 
                             await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.UserName == EmailOrUsernameOrPassword));
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    var confirEmailResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
                    return confirEmailResult;
                }
                return IdentityResult.Failed(new UnexpectedError("Email is already confirmed"));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));

        }

        public override async Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(int userId, string phoneConfirmationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.PhoneNumber != null)
                {
                    var confirmPhoneNumberResult = await _userManager.VerifyChangePhoneNumberTokenAsync(user, phoneConfirmationToken, user.PhoneNumber);
                    if (confirmPhoneNumberResult == true)
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("Invalid token"));
                }
                return IdentityResult.Failed(new NotFoundError("OwnerUser phone number not found"));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }


        public override async Task<IdentityResult> ConfirmPasswordResetToken(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Email == usernameOrEmailOrPhoneNumber)) ??
                             await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.UserName == usernameOrEmailOrPhoneNumber));
            if (user != null)
            {
                var passwordResetResult = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);
                return passwordResetResult;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.UserName == oldUsername)
                {
                    user.UserName = newUsername;
                    await _userRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("Wrong username"));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return changePasswordResult;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> SetUnconfirmedPhoneNumber(int userId, string newPhoneNumber)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.PhoneNumber = newPhoneNumber;
                await _userRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }


        public override async Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Email == userLoginDto.UsernameOrEmailOrPhoneNumber).Include(user => user.UserPreference)) ??
                             await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.UserName == userLoginDto.UsernameOrEmailOrPhoneNumber).Include(user => user.UserPreference));

            if (user != null)
            {
                var twoFactorSignInResult = await _signInManager.TwoFactorSignInAsync(provider, twoFactorToken, false, true);
                if (twoFactorSignInResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); 
                    return twoFactorSignInResult.ToIdentityResult();
                }
                return twoFactorSignInResult.ToIdentityResult();
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Email == userLoginDto.UsernameOrEmailOrPhoneNumber)) ??
                             await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.UserName == userLoginDto.UsernameOrEmailOrPhoneNumber));
            if (user != null)
            {
                if (await _signInManager.CanSignInAsync(user))
                {
                    var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, true);                   
                    if (passwordSignInResult.RequiresTwoFactor)
                    {
                        return IdentityResult.Failed(new UnauthorizedError("Two factor authentication required, Please choose your two-factor provider"));
                    }
                    return passwordSignInResult.ToIdentityResult();               
                }
                await _generalSender.GeneralAuthenticationSend(user, MailType.ConfirmEmail, null, null, null, null);
                return IdentityResult.Failed(new UnauthorizedError("Account is not confirmed. Your confirmation code has sent"));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;
        }


        public override async Task<IdentityResult> ChooseProviderAndSendToken
            (int userId, string provider, MailType? mailType, SmsType? smsType, string? newEmail, string? newPhoneNumber, string? newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                switch (provider)
                {
                    case "Email":
                        if (mailType != null)
                        {
                            return await _generalSender.GeneralAuthenticationSend(user, mailType, null, null, newEmail, newPassword);
                        }
                        return IdentityResult.Failed(new NotFoundError("Mail type is required for email provider"));
                    case "Phone":
                        if (smsType != null)
                        {
                            return await _generalSender.GeneralAuthenticationSend(user, null, smsType, newPhoneNumber, null, newPassword);
                        }
                         return IdentityResult.Failed(new NotFoundError("Sms type is required for phone provider"));
                    default:
                        return IdentityResult.Failed(new NotFoundError("Invalid provider"));
                }
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.UserRegisterDto_To_User();
            var createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);         
            if (createUserResult.Succeeded)
            {
                var result = await _generalSender.GeneralAuthenticationSend(user, MailType.ConfirmEmail, null, null, null, null);
                if (result.Succeeded!) { return IdentityResult.Failed(result.Errors.ToArray()); }
                //Default preference
                user.UserPreference = new UserPreference
                {
                    Theme = "Light",
                    PostPerPage = 10,
                    EntryPerPage = 10,
                    OwnerUserId = user.Id
                };
                var claims = new List<Claim>
                  {
                     new Claim("Theme",user.UserPreference.Theme),
                     new Claim("PostPerPage", user.UserPreference.PostPerPage.ToString()),
                     new Claim("EntryPerPage", user.UserPreference.EntryPerPage.ToString())
                  };
                await _userManager.AddClaimsAsync(user, claims);
                await _userManager.AddToRoleAsync(user, "TempUser");
                await _userRepository.SaveChangesAsync();
                return createUserResult;
            }
            return createUserResult;
        }
    }
}

