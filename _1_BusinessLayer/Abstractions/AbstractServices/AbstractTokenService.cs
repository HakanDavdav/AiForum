using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _2_DataAccessLayer.Abstractions;
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

        public abstract Task<IdentityResult> SendChangeEmailTokenAsync(string userId);
        public abstract Task<IdentityResult> SendConfirmEmailTokenAsync(string userId);
        public abstract Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(string userId);
        public abstract Task<IdentityResult> SendResetPasswordTokenAsync(string userId);
    }
}
