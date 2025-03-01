using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractTools.ITools
{
    public interface ITokenSender
    {
        Task<IdentityResult> SendEmail_EmailChangeTokenAsync(User user, string newMail);
        Task<IdentityResult> SendEmail_EmailConfirmationTokenAsync(User user);
        Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user);
        Task <IdentityResult> SendEmail_TwoFactorTokenAsync(User user);


        Task<IdentityResult> SendSms_PhoneNumberChangeTokenAsync(User user, string newPhoneNumber);
        Task<IdentityResult> SendSms_PhoneNumberConfirmationTokenAsync(User user);
        Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user);
        Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user);
    }
}