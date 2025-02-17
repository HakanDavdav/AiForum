using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.Generic;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractUserService : EntityAbstractGenericBaseService<User>
    {
        protected readonly AbstractAuthenticationService _authenticationService;
        protected readonly AbstractMailService _mailService;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserService(AbstractAuthenticationService authenticationService, 
            AbstractMailService mailService, AbstractUserRepository userRepository, 
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public abstract Task<User> getByName(string name);
        public abstract Task<IdentityResult> ChangePassword(int id);
        public abstract Task<Boolean> EditProfile(int id, UserProfileDto userProfileDto);
        public abstract Task<IdentityResult> Register(UserRegisterDto userRegistered);
        public abstract Task<SignInResult> Login(UserLoginDto userLogged);
        public abstract Task<IdentityResult> Logout();
        public abstract Task<IdentityResult> DeleteProfile(int id);
        public abstract bool ConfirmEmail(int code, int id);
    }
}
