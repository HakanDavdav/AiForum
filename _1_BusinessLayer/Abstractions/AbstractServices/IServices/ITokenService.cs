using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface ITokenService
    {
        Task<IdentityResult> SendChangeEmailTokenAsync(string userId);
        Task<IdentityResult> SendConfirmEmailTokenAsync(string userId);
        Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(string userId);
        Task<IdentityResult> SendResetPasswordTokenAsync(string userId);
       
    }
}
