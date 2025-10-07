using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(Logout),
                ["UserId"] = userId,
                ["TransactionId"] = Guid.NewGuid()
            }))
            {
                _logger.LogInformation("{Handler}.{Method}: Logging out actor {UserId}", this.GetType().Name, methodName, userId);
                await _signInManager.SignOutAsync();
                _logger.LogInformation("{Handler}.{Method}: Logout completed for actor {UserId}", this.GetType().Name, methodName, userId);

                return IdentityResult.Success;
            }
        }


        public async Task<IdentityResult> Login(string username, string email, string password)
        {
            using var logger = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(Login),
                ["Username"] = username,
                ["Email"] = email,
                ["TransactionId"] = Guid.NewGuid()
            });
            _logger.LogInformation("{Handler}.{Method}: Attempting login for user {Username} or email {Email}", this.GetType().Name, nameof(Login), username, email);
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.UserName == username), _logger)
                     ?? await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == email), _logger);
            if (user == null)
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Invalid username or email."));

            var result = await _signInManager.PasswordSignInAsync(user, password, true, lockoutOnFailure: false);
            return result.ToIdentityResult();
        }

        public async Task<IdentityResult> ActivateTwoFactorResult(Guid userId)
        {
            using var logger = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(ActivateTwoFactorResult),
                ["UserId"] = userId,
                ["TransactionId"] = Guid.NewGuid()
            });
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId), _logger);
            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            return result;
        }

        public async Task<IdentityResult> DisableTwoFactorResult(Guid userId)
        {
            using var logger = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(DisableTwoFactorResult),
                ["UserId"] = userId,
                ["TransactionId"] = Guid.NewGuid()
            });
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId), _logger);
            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            return result;
        }

        public async Task<IdentityResult> ConfirmChangeEmailToken(Guid userId, string newEmail, string changeEmailToken)
        {
            using var logger = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(ConfirmChangeEmailToken),
                ["UserId"] = userId,
                ["TransactionId"] = Guid.NewGuid()
            });
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Id == userId), _logger);
            if (user == null)
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            var result = await _userManager.ChangeEmailAsync(user, newEmail, changeEmailToken);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailConfirmationToken(string emailOrUsername, string emailConfirmationToken)
        {
            using var logger = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(ConfirmEmailConfirmationToken),
                ["EmailOrUsername"] = emailOrUsername,
                ["TransactionId"] = Guid.NewGuid()
            });
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername), _logger);
            if (user == null)
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found."));
            var result = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            return result;
        }


    }
}
