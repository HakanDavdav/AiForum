using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class TokenService : AbstractTokenService
    {
        public TokenService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository) : base(tokenSender, userRepository)
        {
        }

        public override Task<IdentityResult> SendChangeEmailTokenAsync(string userId)
        {
            _tokenSender
        }

        public override Task<IdentityResult> SendConfirmEmailTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> SendResetPasswordTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
