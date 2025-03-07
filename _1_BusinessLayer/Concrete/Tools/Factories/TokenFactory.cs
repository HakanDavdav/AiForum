using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractFactories;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace _1_BusinessLayer.Concrete.Tools.Factories
{
    public class TokenFactory : AbstractTokenFactory
    {
        public TokenFactory(AbstractUserRepository userRepository, UserManager<User> userManager)
            : base(userRepository, userManager)
        {
        }

        public override async Task<string> CreateChangeEmailTokenAsync(User user, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public override async Task<string> CreateConfirmPhoneNumberTokenAsync(User user, string phoneNumber)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public override async Task<string> CreateMailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        public override async Task<string> CreatePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public override async Task<string> CreateTwoFactorTokenAsync(User user,string provider)
        {
           return await _userManager.GenerateTwoFactorTokenAsync(user, provider);
        }
    }
}
