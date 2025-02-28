using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractTokenService : ITokenService
    {
        protected readonly AbstractTokenSender _tokenSender;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractTokenService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository)
        {
            _tokenSender = tokenSender;
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> SendChangeEmailTokenAsync(User user, string newEmail);
        public abstract Task<IdentityResult> SendConfirmEmailTokenAsync(User user);
        public abstract Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(User user);
        public abstract Task<IdentityResult> SendResetPasswordTokenAsync(User user);
    }
}
