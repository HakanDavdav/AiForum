using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface ITokenService
    {
        Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(User user,string newEmail);
        Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(User user);
        Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user);
        Task<IdentityResult> SendEmail_TwoFactorTokenAsync(User user);

        Task<IdentityResult> SendSms_ChangePhoneNumberTokenAsync(User user,string newPhoneNumber);
        Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(User user);
        Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user);
        Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user);


    }
}
