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
using _2_DataAccessLayer.Concrete.Repositories;
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
            return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
        }

        public override async Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(int id, string newEmail)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var tokenResult = await _tokenSender.SendEmail_EmailChangeTokenAsync(user, newEmail);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }



        public override async Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
        }

        public override async Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var tokenResult = await _tokenSender.SendEmail_EmailConfirmationTokenAsync(user);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }



        public override async Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user)
        {
            var tokenResult = await _tokenSender.SendEmail_ResetPasswordTokenAsync(user);
            if (tokenResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
        }

        public override async Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var tokenResult = await _tokenSender.SendEmail_ResetPasswordTokenAsync(user);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }




        public override async Task<IdentityResult> SendEmail_TwoFactorTokenAsync(User user)
        {
            if (user.TwoFactorEnabled == true)
            {
                var tokenResult = await _tokenSender.SendEmail_TwoFactorTokenAsync(user);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new UnauthorizedError("User does not activated two factor activation"));
        }

        public override async Task<IdentityResult> SendEmail_TwoFactorTokenAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                if (user.TwoFactorEnabled == true)
                {
                    var tokenResult = await _tokenSender.SendEmail_TwoFactorTokenAsync(user);
                    if (tokenResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not activated two factor activation"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }




        public override async Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(User user, string newPhoneNumber)
        {
            if (user.PhoneNumber != null)
            {
                var tokenResult = await _tokenSender.SendSms_PhoneNumberConfirmationTokenAsync(user);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new NotFoundError("User does not have any phone number"));
        }

        public override async Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(int id, string newPhoneNumber)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                if (user.PhoneNumber != null)
                {
                    var tokenResult = await _tokenSender.SendSms_PhoneNumberConfirmationTokenAsync(user);
                    if (tokenResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }                   
                   return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());                    
                }
                return IdentityResult.Failed(new NotFoundError("User does not have any phone number"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }



        public override async Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user)
        {
            if (user.PhoneNumber != null)
            
                var tokenResult = await _tokenSender.SendSms_ResetPasswordTokenAsync(user);
                if (tokenResult.Succeeded)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
            }
            return IdentityResult.Failed(new NotFoundError("User does not have any phone number"));
        }

        public override async Task<IdentityResult> SendSms_ResetPasswordTokenAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                if (user.PhoneNumber != null)
                {
                    var tokenResult = await _tokenSender.SendSms_ResetPasswordTokenAsync(user);
                    if (tokenResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
                }
                return IdentityResult.Failed(new NotFoundError("User does not have any phone number"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }



        public override async Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user)
        {
            if (user.PhoneNumber != null)
            {
                if (user.TwoFactorEnabled == true)
                {
                    var tokenResult = await _tokenSender.SendSms_TwoFactorTokenAsync(user);
                    if (tokenResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not activated two factor activation"));
            }
            return IdentityResult.Failed(new NotFoundError("User dos not have any phone number"));
        }

        public override async Task<IdentityResult> SendSms_TwoFactorTokenAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user != null)
            {
                if (user.PhoneNumber != null)
                {
                    if(user.TwoFactorEnabled == true)
                    {
                        var tokenResult = await _tokenSender.SendSms_TwoFactorTokenAsync(user);
                        if (tokenResult.Succeeded)
                        {
                            return IdentityResult.Success;
                        }
                        return IdentityResult.Failed(tokenResult.Errors.Concat(new[] { new UnexpectedError("Token is not sent successfully") }).ToArray());
                    }
                    return IdentityResult.Failed(new UnauthorizedError("User does not activated two factor activation"));
                }
                return IdentityResult.Failed(new NotFoundError("User dos not have any phone number"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }
    }
}
