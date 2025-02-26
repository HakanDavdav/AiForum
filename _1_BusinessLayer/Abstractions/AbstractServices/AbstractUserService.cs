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


namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractUserService
    {
        protected readonly AbstractAuthenticationManager _authenticationManager;
        protected readonly AbstractMailManager _mailManager;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserService(AbstractAuthenticationManager authenticationManager, 
            AbstractMailManager mailManager, AbstractUserRepository userRepository, 
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _authenticationManager = authenticationManager;
            _mailManager = mailManager;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public abstract Task<IdentityResult> SearchUserAsync(string name);
        public abstract Task<IdentityResult> ChangePasswordAsync(int id, string password, string newPassword);
        public abstract Task<IdentityResult> EditProfileAsync(int id, UserEditProfileDto userEditProfileDto);
        public abstract Task<IdentityResult> ChangeUserPreferencesAsync(int id, UserPreferences userPreferences);
        public abstract Task<IdentityResult> RegisterAsync(UserRegisterDto userRegistered);
        public abstract Task<IdentityResult> LoginAsync(UserLoginDto userLogged);
        public abstract Task<IdentityResult> LogoutAsync(int id);
        public abstract Task<IdentityResult> DeleteProfileAsync(int id);
        public abstract Task<IdentityResult> ConfirmEmailAsync(int code, int id);
        public abstract Task<IdentityResult> GetUserByIdAsync(int id);
        public abstract Task<IdentityResult> ChangeEmailAsync(int id, string email);
        public abstract Task<IdentityResult> SendEmailConfirmationCodeAsync(int id);
    }
}
