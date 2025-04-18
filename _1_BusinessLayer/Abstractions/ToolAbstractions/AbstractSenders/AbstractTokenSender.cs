﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders
{
    public abstract class AbstractTokenSender : ITokenSender
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly TokenFactory _tokenFactory;
        protected readonly EmailBodyBuilder _emailBodyBuilder;
        protected readonly SmsBodyBuilder _smsBodyBuilder;

        protected AbstractTokenSender(AbstractUserRepository userRepository,
            TokenFactory tokenFactory,
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
        public abstract Task<IdentityResult> SendSms_PhoneNumberConfirmationTokenAsync(User user, string newPhoneNumber);
        public abstract Task<IdentityResult> Send_ResetPasswordTokenAsync(User user, string provider);
        public abstract Task<IdentityResult> Send_TwoFactorTokenAsync(User user, string provider);
    }
}
