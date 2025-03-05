using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractFactories;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders
{
    public abstract class AbstractTokenSender : ITokenSender
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractTokenFactory _tokenFactory;
        protected readonly EmailBodyBuilder _emailBodyBuilder;
        protected readonly SmsBodyBuilder _smsBodyBuilder;

        protected AbstractTokenSender(AbstractUserRepository userRepository,
            AbstractTokenFactory tokenFactory,
            EmailBodyBuilder emailBodyBuilder,
            SmsBodyBuilder smsBodyBuilder)
        {
            _userRepository = userRepository;
            _tokenFactory = tokenFactory;
            _emailBodyBuilder = emailBodyBuilder;
            _smsBodyBuilder = smsBodyBuilder;
        }

        public abstract Task<IdentityResult> SendEmail_EmailChangeTokenAsync(User user, string newMail);
        public abstract Task<IdentityResult> SendEmail_EmailConfirmationTokenAsync(User user);
        public abstract Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user);
        public abstract Task<IdentityResult> SendEmail_TwoFactorTokenAsync(User user);

        public abstract Task<IdentityResult> SendSms_PhoneNumberConfirmationTokenAsync(User user, string newPhoneNumber);
        public abstract Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user);
        public abstract Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user);
    }
}
