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
    public abstract class AbstractMailManager
    {
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractMailManager(AbstractUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public abstract Task CreateMailConfirmationCodeAsync(User user);
    }
}
