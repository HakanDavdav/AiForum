using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.Senders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;


namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractUserIdentityService : IUserIdentityService
    {
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly GeneralSender _generalSender;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly AbstractGenericCommandHandler _genericCommandHandler;


        protected AbstractUserIdentityService(
            AbstractUserQueryHandler userQueryHandler,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GeneralSender generalSender,
            AbstractGenericCommandHandler genericCommandHandler)
        {
            _userQueryHandler = userQueryHandler;
            _userManager = userManager;
            _signInManager = signInManager;
            _generalSender = generalSender;
            _genericCommandHandler = genericCommandHandler;
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
