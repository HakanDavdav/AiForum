using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Tools.Errors;
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

        public override async Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(User user, string newEmail)
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

        public override async Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(User user)
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

        public override async Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user)
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

        public override async Task<IdentityResult> SendEmail_TwoFactorTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_TwoFactorTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }



        public override async Task<IdentityResult> SendSms_ChangePhoneNumberTokenAsync(User user, string newPhoneNumber)
        {
            if (user.PhoneNumber==null)
            {
                return IdentityResult.Failed(new NotFoundError("User do not have any phone number"));
            }
            var tokenResult = await _tokenSender.SendSms_PhoneNumberChangeTokenAsync(user, newPhoneNumber);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(User user)
        {
            if (user.PhoneNumber == null)
            {
                return IdentityResult.Failed(new NotFoundError("User do not have any phone number"));
            }
            var tokenResult = await _tokenSender.SendSms_PhoneNumberConfirmationTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user)
        {
            if (user.PhoneNumber == null)
            {
                return IdentityResult.Failed(new NotFoundError("User do not have any phone number"));
            }
            var tokenResult = await _tokenSender.SendSms_ResetPasswordTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
        }

        public override async Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user)
        {
            if (user.PhoneNumber == null)
            {
                return IdentityResult.Failed(new NotFoundError("User do not have any phone number"));
            }
            var tokenResult = await _tokenSender.SendSms_TwoFactorTokenAsync(user);
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
