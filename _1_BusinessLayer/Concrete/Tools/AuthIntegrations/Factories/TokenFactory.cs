using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.Factories
{
    public class TokenFactory 
    {
        protected readonly UserManager<User> _userManager;
        public TokenFactory(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateChangeEmailTokenAsync(User user, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> CreateConfirmPhoneNumberTokenAsync(User user, string phoneNumber)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public async Task<string> CreateMailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        public async Task<string> CreatePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<string> CreateTwoFactorTokenAsync(User user, string provider)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(user, provider);
        }
    }
}
