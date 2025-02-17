using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.SideServices
{
    public abstract class AbstractAuthenticationService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractAuthenticationService(AbstractUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public abstract string GenerateJwtToken(User user);

        public abstract bool CheckMail(User user,int code);
        public abstract bool CheckMail(User user);
    }
}
