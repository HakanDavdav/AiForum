using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events.Concrete.AuthenticationEvents;
using _2_DataAccessLayer.Concrete.Cqrs;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;

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
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.UserName == username))
                     ?? await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == email));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for username {Username} or email {Email}", this.GetType().Name, nameof(LoginDefault), username, email);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found"));
            }
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(userIdentity, password, true, lockoutOnFailure: false);
            var passwordSignInIdentityResult = passwordSignInResult.ToIdentityResult();
            if (!passwordSignInIdentityResult.Succeeded)
            {
                _logger.LogWarning("{Handler}.{Method}: Login failed for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(LoginDefault), userIdentity.Id, string.Join(", ", passwordSignInIdentityResult.Errors.Select(e => e.Description)));
                return passwordSignInIdentityResult;
            }
            _logger.LogInformation("{Handler}.{Method}: User {UserId} logged in successfully", this.GetType().Name, nameof(LoginDefault), userIdentity.Id);
            return passwordSignInIdentityResult;
        }


        public async Task<IdentityResult> LoginTwoFactor(Guid userId, string twoFactorToken, string provider)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to two-factor login for user {UserId} using provider {Provider}", this.GetType().Name, nameof(LoginTwoFactor), userId, provider);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
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
            _logger.LogInformation("{Handler}.{Method}: User {UserId} logged in successfully with two-factor authentication", this.GetType().Name, nameof(LoginTwoFactor), userIdentity.Id);
            await _signInManager.SignInAsync(userIdentity, isPersistent: false);
            return twoFactorSignInIdentityResult;
        }

        public async Task<IdentityResult> ActivateTwoFactorAuthentication(Guid userId)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to enable two-factor authentication for user {UserId}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetTwoFactorEnabledAsync(userIdentity, true);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to enable two-factor authentication for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Two-factor authentication enabled for user {UserId}", this.GetType().Name, nameof(ActivateTwoFactorAuthentication), userId);
            return result;
        }

        public async Task<IdentityResult> DisableTwoFactorAuthentication(Guid userId)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to disable two-factor authentication for user {UserId}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetTwoFactorEnabledAsync(userIdentity, false);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to disable two-factor authentication for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Two-factor authentication disabled for user {UserId}", this.GetType().Name, nameof(DisableTwoFactorAuthentication), userId);
            return result;
        }

        public async Task<IdentityResult> ConfirmChangeEmailToken(Guid userId, string newEmail, string changeEmailToken)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to confirm change email for user {UserId}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ChangeEmailAsync(userIdentity, newEmail, changeEmailToken);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change email for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Email changed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmChangeEmailToken), userId);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailConfirmationToken(string emailOrUsername, string emailConfirmationToken)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to confirm email for user with email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), emailOrUsername);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), emailOrUsername);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ConfirmEmailAsync(userIdentity, emailConfirmationToken);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to confirm email for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), userIdentity.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Email confirmed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmEmailConfirmationToken), userIdentity.Id);
            return result;
        }

        public async Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(Guid userId, string phoneNumberConfirmationToken)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to confirm phone number for user {UserId}", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            if (userIdentity.PhoneNumber == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User {UserId} does not have a phone number set", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userId);
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, "User does not have a phone number set."));
            }
            var result = await _userManager.VerifyChangePhoneNumberTokenAsync(userIdentity, phoneNumberConfirmationToken, userIdentity.PhoneNumber);
            if (!result)
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, "Invalid phone number confirmation token."));
            else
                _logger.LogInformation("{Handler}.{Method}: Phone number confirmed successfully for user {UserId}", this.GetType().Name, nameof(ConfirmPhoneNumberConfirmationToken), userIdentity.Id);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ConfirmPasswordResetToken(string emailOrUsername, string passwordResetToken, string newPassword)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to confirm reset password token for user with email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmPasswordResetToken), emailOrUsername);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for email or username {EmailOrUsername}", this.GetType().Name, nameof(ConfirmPasswordResetToken), emailOrUsername);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ResetPasswordAsync(userIdentity, passwordResetToken, newPassword);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to reset password for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ConfirmPasswordResetToken), userIdentity.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Password reset successfully for user {UserId}", this.GetType().Name, nameof(ConfirmPasswordResetToken), userIdentity.Id);
            return result;
        }

        public async Task<IdentityResult> SetUnconfirmedPhoneNumber(Guid userId, string newPhoneNumber)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to set phone number for user {UserId}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.SetPhoneNumberAsync(userIdentity, newPhoneNumber);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to set phone number for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Phone number set successfully for user {UserId}", this.GetType().Name, nameof(SetUnconfirmedPhoneNumber), userId);
            return result;
        }


        public async Task<IdentityResult> DeleteAccount(Guid userId, string password)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to delete account for user {UserId}", this.GetType().Name, nameof(DeleteAccount), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(DeleteAccount), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var passwordValid = await _userManager.CheckPasswordAsync(userIdentity, password);
            if (!passwordValid)
            {
                _logger.LogWarning("{Handler}.{Method}: Invalid password provided for user {UserId} when attempting to delete account", this.GetType().Name, nameof(DeleteAccount), userId);
                return IdentityResult.Failed(new AppError(ErrorType.AuthenticationError, "Invalid password."));
            }
            var result = await _userManager.DeleteAsync(userIdentity);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to delete account for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(DeleteAccount), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Account deleted successfully for user {UserId}", this.GetType().Name, nameof(DeleteAccount), userId);
            return result;
        }

        public async Task<IdentityResult> ChangeUsername(Guid userId, string newUsername, string password)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to change username for user {UserId}", this.GetType().Name, nameof(ChangeUsername), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ChangeUsername), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var passwordValid = await _userManager.CheckPasswordAsync(userIdentity, password);
            if (!passwordValid)
            {
                _logger.LogWarning("{Handler}.{Method}: Invalid password provided for user {UserId} when attempting to change username", this.GetType().Name, nameof(ChangeUsername), userId);
                return IdentityResult.Failed(new AppError(ErrorType.AuthenticationError, "Invalid password."));
            }
            var result = await _userManager.SetUserNameAsync(userIdentity, newUsername);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change username for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ChangeUsername), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Username changed successfully for user {UserId}", this.GetType().Name, nameof(ChangeUsername), userId);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to change password for user {UserId}", this.GetType().Name, nameof(ChangePassword), userId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for userId {UserId}", this.GetType().Name, nameof(ChangePassword), userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var result = await _userManager.ChangePasswordAsync(userIdentity, currentPassword, newPassword);
            if (!result.Succeeded)
                _logger.LogWarning("{Handler}.{Method}: Failed to change password for user {UserId}. Errors: {Errors}", this.GetType().Name, nameof(ChangePassword), userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            else
                _logger.LogInformation("{Handler}.{Method}: Password changed successfully for user {UserId}", this.GetType().Name, nameof(ChangePassword), userId);
            return result;
        }

        public async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to register new user with username {Username} and email {Email}", this.GetType().Name, nameof(Register), userRegisterDto.Username, userRegisterDto.Email);
            var userId = Guid.NewGuid();
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
                PremiumFeatures = 0,
                Theme = ThemeOptions.Light,
                EntryPerPage = 10,
                PostPerPage = 30,
                SocialEmailPreference = true,
                SocialNotificationPreference = true,
            };
            var claims = new List<Claim>
            {
                new Claim(nameof(userSettings.Theme), userSettings.Theme.ToString()),
                new Claim(nameof(userSettings.EntryPerPage), userSettings.EntryPerPage.ToString()),
                new Claim(nameof(userSettings.PostPerPage), userSettings.PostPerPage.ToString()),
                new Claim(nameof(userSettings.PremiumFeatures), userSettings.PremiumFeatures.ToString()),
                new Claim(nameof(userSettings.IsProfileCreated), userSettings.IsProfileCreated.ToString()),
            };
            await _userManager.AddClaimsAsync(userIdentity, claims);
            await _userManager.AddToRoleAsync(userIdentity, "StandardUser");
            await _commandHandler.ManuallyInsertAsync<UserIdentity>(userIdentity);
            await _commandHandler.ManuallyInsertAsync<UserSettings>(userSettings);
            await _commandHandler.ManuallyInsertAsync<User>(user);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: User {UserId} registered successfully", this.GetType().Name, nameof(Register), userId);
            return IdentityResult.Success;

        }

        public async Task<IdentityResult> CreateEmailChangeEventAsync(Guid actorId, string newEmail)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create EmailChange event for actor {ActorId}", this.GetType().Name, nameof(CreateEmailChangeEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateEmailChangeEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GenerateChangeEmailTokenAsync(userIdentity, newEmail);
            var evt = new EmailChange
            {
                ActorId = actorId,
                Token = Guid.NewGuid(), // Identity tokens are string, but your event expects Guid
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(EmailChange),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: EmailChange event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateEmailChangeEventAsync), actorId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateEmailConfirmEventAsync(Guid actorId)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create EmailConfirm event for actor {ActorId}", this.GetType().Name, nameof(CreateEmailConfirmEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateEmailConfirmEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userIdentity);
            var evt = new EmailConfirm
            {
                ActorId = actorId,
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(EmailConfirm),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: EmailConfirm event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateEmailConfirmEventAsync), actorId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateResetPasswordEventAsync(Guid actorId, AuthenticationStrategy strategy)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create ResetPassword event for actor {ActorId}", this.GetType().Name, nameof(CreateResetPasswordEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateResetPasswordEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);
            var evt = new ResetPassword
            {
                ActorId = actorId,
                Token = Guid.NewGuid(),
                Strategy = strategy,
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(ResetPassword),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: ResetPassword event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateResetPasswordEventAsync), actorId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateTwoFactorLoginEventAsync(Guid actorId, AuthenticationStrategy strategy, string provider)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create TwoFactorLogin event for actor {ActorId}", this.GetType().Name, nameof(CreateTwoFactorLoginEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateTwoFactorLoginEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(userIdentity, provider);
            var evt = new TwoFactorLogin
            {
                ActorId = actorId,
                Token = Guid.NewGuid(),
                Strategy = strategy,
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(TwoFactorLogin),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: TwoFactorLogin event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateTwoFactorLoginEventAsync), actorId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateConfirmPhoneNumberEventAsync(Guid actorId, string phoneNumber)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create ConfirmPhoneNumber event for actor {ActorId}", this.GetType().Name, nameof(CreateConfirmPhoneNumberEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateConfirmPhoneNumberEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(userIdentity, phoneNumber);
            var evt = new ConfirmPhoneNumber
            {
                ActorId = actorId,
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(ConfirmPhoneNumber),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: ConfirmPhoneNumber event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateConfirmPhoneNumberEventAsync), actorId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateChangePhoneNumberEventAsync(Guid actorId, string phoneNumber)
        {
            _logger.LogInformation("{Handler}.{Method}: Attempting to create ChangePhoneNumber event for actor {ActorId}", this.GetType().Name, nameof(CreateChangePhoneNumberEventAsync), actorId);
            var userIdentity = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == actorId));
            if (userIdentity == null)
            {
                _logger.LogWarning("{Handler}.{Method}: User not found for actorId {ActorId}", this.GetType().Name, nameof(CreateChangePhoneNumberEventAsync), actorId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            }
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(userIdentity, phoneNumber);
            var evt = new ChangePhoneNumber
            {
                ActorId = actorId,
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(ChangePhoneNumber),
                Payload = JsonSerializer.Serialize(evt),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            _logger.LogInformation("{Handler}.{Method}: ChangePhoneNumber event created and outbox message inserted for actor {ActorId}", this.GetType().Name, nameof(CreateChangePhoneNumberEventAsync), actorId);
            return IdentityResult.Success;
        }

    }
}
