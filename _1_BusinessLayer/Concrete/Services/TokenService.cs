using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class TokenService : AbstractTokenService
    {
        public TokenService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository) : base(tokenSender, userRepository)
        {
        }

        public override async Task<IdentityResult> SendChangeEmailTokenAsync(User user, string newEmail)
        {
            var tokenResult = await _tokenSender.SendEmail_EmailChangeTokenAsync(user, newEmail);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendConfirmEmailTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendResetPasswordTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_ResetPasswordTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }
    }
}
