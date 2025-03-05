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

        Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(User user, string newPhoneNumber);
        Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user);
        Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user);


        Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(int id, string newEmail);
        Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(int id);
        Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(int id);
        Task<IdentityResult> SendEmail_TwoFactorTokenAsync(int id);

        Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(int id, string newPhoneNumber);
        Task<IdentityResult> SendSms_ResetPasswordTokenAsync(int id);
        Task<IdentityResult> SendSms_TwoFactorTokenAsync(int id);


    }
}
