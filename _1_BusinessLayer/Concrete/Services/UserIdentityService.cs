using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserIdentityService : AbstractUserIdentityService
    {
        public UserIdentityService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository,
            UserManager<User> userManager, SignInManager<User> signInManager,
            AbstractUserPreferenceRepository userPreferenceRepository)
            : base(tokenSender, userRepository, userManager, signInManager, userPreferenceRepository)
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
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DisableTwoFactorAuthentication(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
                return twoFactorResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ChangeEmail(int userId, string newEmail, string changeEmailToken)
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

        public override async Task<IdentityResult> ConfirmEmail(string emailConfirmationToken, string EmailOrUsernameOrPassword)
        {
            var user = await _userRepository.GetByEmailAsync(EmailOrUsernameOrPassword) ??
                       await _userRepository.GetByUsernameAsync(EmailOrUsernameOrPassword);
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

        public override async Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken)
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
                return IdentityResult.Failed(new NotFoundError("User phone number not found"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> SetPhoneNumber(int userId, string newPhoneNumber)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.PhoneNumber = newPhoneNumber;
                await _userRepository.UpdateAsync(user);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }


        public override async Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginDto.UsernameOrEmailOrPhoneNumber) ??
                       await _userRepository.GetByUsernameAsync(userLoginDto.UsernameOrEmailOrPhoneNumber);
            if (user != null)
            {
                var twoFactorSignInResult = await _signInManager.TwoFactorSignInAsync(provider, twoFactorToken, false, false);
                if (twoFactorSignInResult.Succeeded)
                {
                    var preference = await _userPreferenceRepository.GetByUserIdAsync(user.Id);
                    var claims = new List<Claim>
                    {
                        new Claim("THEME",preference.Theme),
                        new Claim("POST PER PAGE", preference.PostPerPage.ToString()),
                        new Claim("ENTRY PER PAGE", preference.EntryPerPage.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(identity);
                    await _signInManager.SignInAsync(user, isPersistent: false); 
                    return twoFactorSignInResult.ToIdentityResult();
                }
                return twoFactorSignInResult.ToIdentityResult();
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginDto.UsernameOrEmailOrPhoneNumber) ??
                       await _userRepository.GetByUsernameAsync(userLoginDto.UsernameOrEmailOrPhoneNumber);
            if(user != null)
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
                await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
                return IdentityResult.Failed(new UnauthorizedError("Account is not confirmed. Your confirmation code has sent"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ChooseProviderAndSendToken
            (int userId, string provider, string operation, string newEmail, string newPhoneNumber)

        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }

            switch (operation)
            {              
                case "ChangeEmail":
                    if (string.IsNullOrEmpty(newEmail))
                        return IdentityResult.Failed(new ValidationError("New email is required"));
                    await _tokenSender.SendEmail_EmailChangeTokenAsync(user, newEmail);
                    break;

                case "ConfirmPhoneNumber":
                    if (string.IsNullOrEmpty(newPhoneNumber))
                        return IdentityResult.Failed(new ValidationError("New phone number is required"));
                    await _tokenSender.SendSms_PhoneNumberConfirmationTokenAsync(user, newPhoneNumber);
                    break;

                default:
                    return IdentityResult.Failed(new ForbiddenError("Invalid operation"));
            }

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> ChooseProviderAndSendToken
            (string provider, string operation, string usernameOrEmailOrPhoneNumber)

        {
            var user = await _userRepository.GetByEmailAsync(usernameOrEmailOrPhoneNumber) ??
                       await _userRepository.GetByUsernameAsync(usernameOrEmailOrPhoneNumber);
            if (user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }

            switch (operation)
            {
                case "TwoFactor":
                    await _tokenSender.Send_TwoFactorTokenAsync(user, provider);
                    break;

                case "ResetPassword":
                    await _tokenSender.Send_ResetPasswordTokenAsync(user, provider);
                    break;              

                default:
                    return IdentityResult.Failed(new ForbiddenError("Invalid operation"));
            }

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;
        }


        public override async Task<IdentityResult> PasswordReset(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword)
        {
            var user = await _userRepository.GetByEmailAsync(usernameOrEmailOrPhoneNumber) ??
                      await _userRepository.GetByUsernameAsync(usernameOrEmailOrPhoneNumber);
            if (user != null)
            {
                var passwordResetResult = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);
                return passwordResetResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.UserRegisterDto_To_User();
            if (user != null)
            {
                var createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);
                await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
                if (createUserResult.Succeeded)
                {
                    //Default preference
                    await _userPreferenceRepository.InsertAsync(new UserPreference
                    {
                        UserId = user.Id
                    });
                    await _userManager.AddToRoleAsync(user, "StandardUser");
                    return createUserResult;
                }
                return createUserResult;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.UserName == oldUsername)
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

