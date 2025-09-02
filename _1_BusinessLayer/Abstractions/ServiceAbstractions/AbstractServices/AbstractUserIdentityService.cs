using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Senders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;


namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractUserIdentityService : IUserIdentityService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractUserPreferenceRepository _userPreferenceRepository;
        protected readonly GeneralSender _generalSender;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserIdentityService( AbstractUserRepository userRepository,
            UserManager<User> userManager, SignInManager<User> signInManager, AbstractUserPreferenceRepository userPreferenceRepository,
            GeneralSender generalSender)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _userPreferenceRepository = userPreferenceRepository;
            _generalSender = generalSender;

        }

        public abstract Task<IdentityResult> ActivateTwoFactorAuthentication(int userId);
        public abstract Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(int userId, string phoneConfirmationToken);
        public abstract Task<IdentityResult> ConfirmChangeEmailToken(int userId, string newEmail, string changeEmailToken);
        public abstract Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        public abstract Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername);
        public abstract Task<IdentityResult> ChooseProviderAndSendToken(int userId, string provider, MailTypes.MailType? mailType, SmsTypes.SmsType? smsType, string newEmail, string newPhoneNumber, string newPassword);
        public abstract Task<IdentityResult> ConfirmEmailConfirmationToken(string emailConfirmationToken, string usernameEmailOrPhoneNumber);
        public abstract Task<IdentityResult> DisableTwoFactorAuthentication(int userId);
        public abstract Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto);
        public abstract Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider);
        public abstract Task<IdentityResult> Logout();
        public abstract Task<IdentityResult> ConfirmPasswordResetToken(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword);
        public abstract Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
        public abstract Task<IdentityResult> SetUnconfirmedPhoneNumber(int userId, string newPhoneNumber);
    }
}
