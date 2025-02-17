using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.SideServices
{
    public abstract class AbstractMailService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractMailService(AbstractUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public abstract void CreateMailConfirmationCode(User user);
    }
}
