using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;


namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractUserIdentityService : IUserIdentityService
    {
        protected readonly AbstractTokenSender _tokenSender;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractUserPreferenceRepository _userPreferenceRepository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserIdentityService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository,
            UserManager<User> userManager, SignInManager<User> signInManager, AbstractUserPreferenceRepository userPreferenceRepository)
        {
            _userRepository = userRepository;
            _tokenSender = tokenSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _userPreferenceRepository = userPreferenceRepository;

        }

        public abstract Task<IdentityResult> ActivateTwoFactorAuthentication(int userId);
        public abstract Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken);
        public abstract Task<IdentityResult> ChangeEmail(int userId, string newEmail, string changeEmailToken);
        public abstract Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        public abstract Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername);
        public abstract Task<IdentityResult> ChooseProviderAndSendToken(int userId, string provider, string operation, string newEmail, string newPhoneNumber);
        public abstract Task<IdentityResult> ChooseProviderAndSendToken(string provider, string operation, string usernameOrEmailOrPhoneNumber);
        public abstract Task<IdentityResult> ConfirmEmail(string emailConfirmationToken, string usernameEmailOrPhoneNumber);
        public abstract Task<IdentityResult> DisableTwoFactorAuthentication(int userId);
        public abstract Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto);
        public abstract Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider);
        public abstract Task<IdentityResult> Logout();
        public abstract Task<IdentityResult> PasswordReset(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword);
        public abstract Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
        public abstract Task<IdentityResult> SetPhoneNumber(int userId, string newPhoneNumber);
    }
}
