using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractUserService
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


        public abstract Task<ObjectResult> SearchUserAsync(string name);
        public abstract Task<ObjectResult> ChangePasswordAsync(int id, string password);
        public abstract Task<ObjectResult> EditProfileAsync(int id, UserProfileDto userProfileDto);
        public abstract Task<ObjectResult> ChangeUserPreferencesAsync(int id, UserPreferences userPreferences);
        public abstract Task<ObjectResult> RegisterAsync(UserRegisterDto userRegistered);
        public abstract Task<ObjectResult> LoginAsync(UserLoginDto userLogged);
        public abstract Task<ObjectResult> LogoutAsync(int id);
        public abstract Task<ObjectResult> DeleteProfileAsync(int id);
        public abstract Task<ObjectResult> ConfirmEmailAsync(int code, int id);
        public abstract Task<ObjectResult> GetUserByIdAsync(int id);
        public abstract Task<ObjectResult> ChangeEmailAsync(int id, string email);
    }
}
