using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractTokenService : ITokenService
    {
        protected readonly AbstractTokenSender _tokenSender;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractTokenService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository)
        {
            _tokenSender = tokenSender;
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> SendEmail_ChangeEmailTokenAsync(int id, string newEmail);
        public abstract Task<IdentityResult> SendEmail_ConfirmEmailTokenAsync(int id);
        public abstract Task<IdentityResult> SendSms_ConfirmPhoneNumberTokenAsync(int id, string newPhoneNumber);
        public abstract Task<IdentityResult> Send_ResetPasswordTokenAsync(int id, string provider);
    }
}
