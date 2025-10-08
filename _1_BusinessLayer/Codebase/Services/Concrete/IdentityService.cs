using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Cqrs;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace _1_BusinessLayer.Concrete.Services._Concrete
{
    public class IdentityService
    {
        SignInManager<UserIdentity> _signInManager;
        UserManager<UserIdentity> _userManager;
        GenericQueryHandler _queryHandler;
        GenericCommandHandler _commandHandler;
        ILogger<IdentityService> _logger;

        public IdentityService(SignInManager<UserIdentity> signInManager, UserManager<UserIdentity> userManager,
            GenericQueryHandler queryHandler, GenericCommandHandler commandHandler, ILogger<IdentityService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task<IdentityResult> Logout(Guid userId)
        {

            _logger.LogInformation("{Handler}.{Method}: Logging out actor {UserId}", this.GetType().Name, nameof(Logout), userId);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("{Handler}.{Method}: Actor {UserId} logged out successfully", this.GetType().Name, nameof(Logout), userId);
            return IdentityResult.Success;
        }


        public async Task<IdentityResult> LoginDefault(string username, string email, string password)
        {

            _logger.LogInformation("{Handler}.{Method}: Attempting login for user {Username} or email {Email}", this.GetType().Name, nameof(LoginDefault), username, email);
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.UserName == username))
                     ?? await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == email));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for username {Username} or email {Email}", this.GetType().Name, nameof(LoginDefault), username, email);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found"));
            }
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, password, true, lockoutOnFailure: false);
            var passwordSignInIdentityResult = passwordSignInResult.ToIdentityResult();
            if (!passwordSignInIdentityResult.Succeeded)
            {
                _logger.LogWarning("{Handler}.{Method}: Login failed for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(LoginDefault), user.Id, string.Join(", ", passwordSignInIdentityResult.Errors.Select(e => e.Description)));
                return passwordSignInIdentityResult;
            }
            _logger.LogInformation("{Handler}.{Method}: User {UserId} logged in successfully", this.GetType().Name, nameof(LoginDefault), user.Id);
            return passwordSignInIdentityResult;
        }


        public async Task<IdentityResult> LoginTwoFactor(Guid userId, string twoFactorToken, string provider)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting two-factor login for user {UserId} using provider {Provider}", this.GetType().Name, nameof(LoginTwoFactor), userId, provider);
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(LoginTwoFactor), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var twoFactorSignInResult = await _signInManager.TwoFactorSignInAsync(provider, twoFactorToken, true, false);
            var twoFactorSignInIdentityResult = twoFactorSignInResult.ToIdentityResult();
            if (!twoFactorSignInIdentityResult.Succeeded)
            {
                _logger.LogWarning("{Handler}.{Method}: Two-factor login failed for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(LoginTwoFactor), userId, string.Join(", ", twoFactorSignInIdentityResult.Errors.Select(e => e.Description)));
                return twoFactorSignInIdentityResult;
            }
            _logger.LogInformation("{Handler}.{Method}: User {UserId} logged in successfully with two-factor authentication", this.GetType().Name, nameof(LoginTwoFactor), user.Id);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return twoFactorSignInIdentityResult;
        }

        public async Task<IdentityResult> ActivateTwoFactorAuthentication(Guid userId)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to enable two-factor authentication for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Two-factor authentication enabled for user {UserId}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId);
            return result;
        }

        public async Task<IdentityResult> DisableTwoFactorAuthentication(Guid userId)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to disable two-factor authentication for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Two-factor authentication disabled for user {UserId}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId);
            return result;
        }

        public async Task<IdentityResult> ConfirmChangeEmailToken(Guid userId, string newEmail, string changeEmailToken)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ChangeEmailAsync(user, newEmail, changeEmailToken);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change email for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Email changed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailConfirmationToken(string emailOrUsername, string emailConfirmationToken)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), emailOrUsername);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to confirm email for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Email confirmed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), user.Id);
            return result;
        }

        public async Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(Guid userId, string phoneNumberConfirmationToken)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            if (user.PhoneNumber == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User {UserId} does not have a phone number set", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, "User does not have a phone number set."));
            }
            var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, phoneNumberConfirmationToken, user.PhoneNumber);
            if (!result)
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, "Invalid phone number confirmation token."));
            else
                _logger.LogInformation("{Handler}.{Method}: Phone number confirmed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), user.Id);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ConfirmPasswordResetToken(string emailOrUsername, string passwordResetToken, string newPassword)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmPasswordResetToken), emailOrUsername);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, newPassword);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to reset password for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmPasswordResetToken), user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Password reset successfully for user {UserId}", this.GetType().Name, nameof(ConfirmPasswordResetToken), user.Id);
            return result;
        }

        public async Task<IdentityResult> SetUnconfirmedPhoneNumber(Guid userId, string newPhoneNumber)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetPhoneNumberAsync(user, newPhoneNumber);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to set phone number for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Phone number set successfully for user {UserId}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId);
            return result;
        }


        public async Task<IdentityResult> DeleteAccount(Guid userId, string password)
        {

            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(DeleteAccount), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                _logger.LogWarning("{Handler}.{Method}: Invalid password provided for user {UserId} when attempting to delete account", this.GetType().Name, nameof(DeleteAccount), userId);
                return IdentityResult.Failed(new AppError(ErrorType.AuthenticationError, "Invalid password."));
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to delete account for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(DeleteAccount), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Account deleted successfully for user {UserId}", this.GetType().Name, nameof(DeleteAccount), userId);
            return result;
        }

        public async Task<IdentityResult> ChangeUsername(Guid userId, string newUsername, string password)
        {

            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ChangeUsername), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                _logger.LogWarning("{Handler}.{Method}: Invalid password provided for user {UserId} when attempting to change username", this.GetType().Name, nameof(ChangeUsername), userId);
                return IdentityResult.Failed(new AppError(ErrorType.AuthenticationError, "Invalid password."));
            }
            var result = await _userManager.SetUserNameAsync(user, newUsername);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change username for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ChangeUsername), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Username changed successfully for user {UserId}", this.GetType().Name, nameof(ChangeUsername), userId);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (user == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ChangePassword), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change password for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ChangePassword), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Password changed successfully for user {UserId}", this.GetType().Name, nameof(ChangePassword), userId);
            return result;
        }

        public async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var userId = new Guid();
            var user = new User
            {
                ActorId = userId,
                ActorPoint = 0,
                CreatedAt = DateTime.UtcNow,
                ProfileName = null,
                Bio = null,
            };
            var userIdentity = new UserIdentity
            {
                Id = userId,
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email
            };
            var userSettings = new UserSettings
            {
                ActorId = userId,
                IsProfileCreated = false,
                PremiumFeatures = UserFeatures.None,
                Theme = ThemeOptions.Light,
                EntryPerPage = 10,
                PostPerPage = 10,
                SocialEmailPreference = true,
                SocialNotificationPreference = true
            };

        }



    }
}
