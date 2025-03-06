using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractTools.ITools
{
    public interface ITokenSender
    {
        Task<IdentityResult> SendEmail_EmailChangeTokenAsync(User user, string newMail);
        Task<IdentityResult> SendEmail_EmailConfirmationTokenAsync(User user);
        Task<IdentityResult> SendSms_PhoneNumberConfirmationTokenAsync(User user, string newPhoneNumber);



        Task<IdentityResult> Send_ResetPasswordTokenAsync(User user,string provider);
        Task<IdentityResult> Send_TwoFactorTokenAsync(User user, string provider);
    }
}