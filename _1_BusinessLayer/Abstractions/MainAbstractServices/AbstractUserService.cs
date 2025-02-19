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
using Microsoft.AspNetCore.Mvc;

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


        public abstract Task<ObjectResult> getByName(string name);
        public abstract Task<ObjectResult> ChangePassword(int id);
        public abstract Task<ObjectResult> EditProfile(int id, UserProfileDto userProfileDto);
        public abstract Task<ObjectResult> Register(UserRegisterDto userRegistered);
        public abstract Task<ObjectResult> Login(UserLoginDto userLogged);
        public abstract Task<ObjectResult> Logout();
        public abstract Task<ObjectResult> DeleteProfile(int id);
        public abstract Task<ObjectResult> ConfirmEmail(int code, int id);
    }
}
