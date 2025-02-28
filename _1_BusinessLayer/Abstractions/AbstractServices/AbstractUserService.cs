using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;


namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractUserService : IUserService
    {
        protected readonly AbstractTokenSender _tokenSender;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserService(AbstractTokenSender tokenSender, AbstractUserRepository userRepository,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _tokenSender = tokenSender;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        public abstract Task<IdentityResult> ChangeEmail(int userId,string newEmail ,string changeEmailToken);
        public abstract Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        public abstract Task<IdentityResult> ConfirmEmail(int userId, string confirmEmailToken);
        public abstract Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken);
        public abstract Task<IdentityResult> Login(UserLoginDto userLoginDto, string twoFactorToken);
        public abstract Task<IdentityResult> Logout();
        public abstract Task<IdentityResult> PasswordReset(int userId, string newPassword, string resetPasswordToken);
        public abstract Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
    }
}
