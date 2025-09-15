using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.Extensions.Mappers;
using _1_BusinessLayer.Concrete.Tools.Senders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.SmsTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserIdentityService : AbstractUserIdentityService
    {
        public UserIdentityService(
            AbstractUserQueryHandler userQueryHandler,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GeneralSender generalSender,
            AbstractGenericCommandHandler genericCommandHandler)
            : base(userQueryHandler, userManager, signInManager, generalSender, genericCommandHandler)
        {
        }

        public override async Task<IdentityResult> ActivateTwoFactorAuthentication(int userId)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, true);
            return twoFactorResult;
        }

        public override async Task<IdentityResult> DisableTwoFactorAuthentication(int userId)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            return twoFactorResult;
        }

        public override async Task<IdentityResult> ConfirmChangeEmailToken(int userId, string newEmail, string changeEmailToken)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            var changeEmailResult = await _userManager.ChangeEmailAsync(user, newEmail, changeEmailToken);
            return changeEmailResult;
        }

        public override async Task<IdentityResult> ConfirmEmailConfirmationToken(string emailConfirmationToken, string EmailOrUsernameOrPassword)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Email == EmailOrUsernameOrPassword)) ??
                             await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.UserName == EmailOrUsernameOrPassword));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            if (!await _userManager.IsEmailConfirmedAsync(user))
                return IdentityResult.Failed(new UnauthorizedError("Email is already confirmed"));
            var confirEmailResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            return confirEmailResult;
        }

        public override async Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(int userId, string phoneConfirmationToken)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            if (user.PhoneNumber == null)
                return IdentityResult.Failed(new NotFoundError("User phone number not found"));
            var confirmPhoneNumberResult = await _userManager.VerifyChangePhoneNumberTokenAsync(user, phoneConfirmationToken, user.PhoneNumber);
            if (confirmPhoneNumberResult == true)
                return IdentityResult.Success;
            return IdentityResult.Failed(new UnauthorizedError("Invalid token"));
        }

        public override async Task<IdentityResult> ConfirmPasswordResetToken(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Email == usernameOrEmailOrPhoneNumber)) ??
                             await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.UserName == usernameOrEmailOrPhoneNumber));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            var passwordResetResult = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);
            return passwordResetResult;
        }

        public override async Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            if (user.UserName == newUsername) return IdentityResult.Failed(new UnauthorizedError("New username cannot be the same as the old one"));
            user.UserName = newUsername;
            try
            {
                await _genericCommandHandler.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                return IdentityResult.Failed(new ConflictError("Username already in use"));
            }
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return changePasswordResult;
        }

        public override async Task<IdentityResult> SetUnconfirmedPhoneNumber(int userId, string newPhoneNumber)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            user.PhoneNumber = newPhoneNumber;
            try
            {
                await _genericCommandHandler.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                return IdentityResult.Failed(new ConflictError("Phone number already in use"));
            }
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Email == userLoginDto.UsernameOrEmailOrPhoneNumber).Include(u => u.UserPreference)) ??
                             await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.UserName == userLoginDto.UsernameOrEmailOrPhoneNumber).Include(u => u.UserPreference));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            var twoFactorSignInResult = await _signInManager.TwoFactorSignInAsync(provider, twoFactorToken, false, true);
            if (!twoFactorSignInResult.Succeeded)
                return twoFactorSignInResult.ToIdentityResult();
            await _signInManager.SignInAsync(user, isPersistent: false);
            return twoFactorSignInResult.ToIdentityResult();
        }

        public override async Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Email == userLoginDto.UsernameOrEmailOrPhoneNumber)) ??
                             await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.UserName == userLoginDto.UsernameOrEmailOrPhoneNumber));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            if (!await _signInManager.CanSignInAsync(user)) return IdentityResult.Failed(new UnauthorizedError("Account is not confirmed. Your confirmation code has sent"));
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, true);
            if (passwordSignInResult.RequiresTwoFactor)
                return IdentityResult.Failed(new UnauthorizedError("Two factor authentication required, Please choose your two-factor provider"));
            return passwordSignInResult.ToIdentityResult();
        }

        public override async Task<IdentityResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> ChooseProviderAndSendToken
            (int userId, string provider, MailType? mailType, SmsType? smsType, string? newEmail, string? newPhoneNumber, string? newPassword)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
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

        public override async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.UserRegisterDto_To_User();
            var createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!createUserResult.Succeeded)
                return createUserResult;

            var result = await _generalSender.GeneralAuthenticationSend(user, MailType.ConfirmEmail, null, null, null, null);
            if (result.Succeeded!)
                return IdentityResult.Failed(result.Errors.ToArray());
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
            await _genericCommandHandler.SaveChangesAsync();
            return createUserResult;
        }
    }
}

