using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.Generic;
using _1_BusinessLayer.Abstractions.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractUserService : EntityAbstractGenericBaseService<User>
    {
        protected readonly AbstractAuthenticationService _authenticationService;
        protected readonly AbstractAuthorizationService _authorizationService;
        protected readonly AbstractMailService _mailService;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractUserService(AbstractAuthenticationService authenticationService, AbstractAuthorizationService authorizationService,
            AbstractMailService mailService, AbstractUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _mailService = mailService;
            _userRepository = userRepository;
        }
        public abstract User TgetByName(string name);
        public abstract void ChangePassword(string url);
        public abstract void ChangeUsername(string url);
        public abstract void ChangePP(int id);
        public abstract void Register(string mail, string password);
        public abstract void Login(User user);
    }
}
