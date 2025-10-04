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
            GenericQueryHandler queryHandler, GenericCommandHandler commandHandler, ILogger logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task<IdentityResult> Logout(int actorId, ILogger logger, [CallerMemberName] string methodName = "")
        {
            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = methodName,
                ["ActorId"] = actorId,
                ["TransactionId"] = Guid.NewGuid() 
            }))
            {
                logger.LogInformation("{Handler}.{Method}: Logging out actor {ActorId}", this.GetType().Name, methodName, actorId);

                await _signInManager.SignOutAsync();

                logger.LogInformation("{Handler}.{Method}: Logout completed for actor {ActorId}", this.GetType().Name, methodName, actorId);

                return IdentityResult.Success;
            }
        }


        public async Task<IdentityResult> Login(string username,string email ,string password, [CallerMemberName] string methodName = "")
        {
            var user =  await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.UserName == username), _logger)
                     ?? await _queryHandler.GetBySpecificPropertySingularAsync<UserIdentity>(q => q.Where(u => u.Email == email),_logger);
            if (user == null)       
                return IdentityResult.Failed(new AppError(ErrorType.NotFound,"Invalid username or email."));

            var result = await _signInManager.PasswordSignInAsync(user, password,true , lockoutOnFailure: false);
            return result.ToIdentityResult();
        }
    }
}
