using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.SideServices
{
    public abstract class AbstractAuthenticationManager
    {
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractAuthenticationManager(AbstractUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> ValidateEmailAsync(User user,int code);
        public abstract IdentityResult CheckEmailValidation(User user);
    }
}
