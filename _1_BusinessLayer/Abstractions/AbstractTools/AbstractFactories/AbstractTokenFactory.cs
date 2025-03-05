using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractTools.AbstractFactories
{
    public abstract class AbstractTokenFactory : ITokenFactory
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly UserManager<User> _userManager;
        protected AbstractTokenFactory(AbstractUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public abstract Task<string> CreateChangeEmailTokenAsync(User user, string newEmail);
        public abstract Task<string> CreateConfirmPhoneNumberTokenAsync(User user, string phoneNumber);
        public abstract Task<string> CreateMailConfirmationTokenAsync(User user);
        public abstract Task<string> CreatePasswordResetTokenAsync(User user);
        public abstract Task<string> CreateTwoFactorTokenAsync(User user);
    }
}
