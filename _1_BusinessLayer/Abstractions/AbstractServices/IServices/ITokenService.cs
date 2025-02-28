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
        Task<IdentityResult> SendChangeEmailTokenAsync(User user,string newEmail);
        Task<IdentityResult> SendConfirmEmailTokenAsync(User user);
        Task<IdentityResult> SendConfirmPhoneNumberTokenAsync(User user);
        Task<IdentityResult> SendResetPasswordTokenAsync(User user);
       
    }
}
