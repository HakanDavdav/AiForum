using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface ITokenService
    {
        Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(int id,string newEmail);
        Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(int id);
        Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(int id, string newPhoneNumber);


        Task<IdentityResult> Send_ResetPasswordTokenAsync(int id, string provider);

    }
}
