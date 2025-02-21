using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos;
using _1_BusinessLayer.Concrete.Mappers;
using _1_BusinessLayer.Concrete.Services.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class UserService : AbstractUserService 
    {
        public UserService(AbstractAuthenticationService authenticationService, 
            AbstractMailService mailService, AbstractUserRepository userRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager) : base(authenticationService, mailService, userRepository, userManager, signInManager)
        {
        }

        public override async Task<ObjectResult> ChangePasswordAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectResult> ConfirmEmailAsync(int code, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                if (_authenticationService.CheckMail(user, code))
                {
                    return new OkObjectResult(new { Message = "Mail is confirmed", boolean = true });
                }
                else
                {
                    return new BadRequestObjectResult(new { Message = "Mail is not confirmed", boolean = false });
                }
                
            }
            
        }

        public override async Task<ObjectResult> DeleteProfileAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                return new OkObjectResult(new { Message = "Deletion succesfull", IdentityResult = result });
            }
          
        }

        public override async Task<ObjectResult> EditProfileAsync(int id, UserProfileDto userProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                user.UserName = userProfileDto.Username;
                user.ImageUrl = userProfileDto.ImageUrl;
                user.City = userProfileDto.City;
                return new OkObjectResult(new { Message = "Profile updated", UserProfileDto = user.UserToUserProfile() });
            }
           
        }

        public override async Task<ObjectResult> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new OkObjectResult(new { Message = "UserId Query successful", User = user });
        }

        public override async Task<ObjectResult> LoginAsync(UserLoginDto userLogged)
        {
            
            var user = await _userRepository.GetByEmailAsync(userLogged.EmailOrUsername)?? 
                       await _userRepository.GetByUsernameAsync(userLogged.EmailOrUsername);
            if (_authenticationService.CheckMail(user))
            {
                var result = await _signInManager.PasswordSignInAsync(userLogged.EmailOrUsername, userLogged.Password, false, false);
                if (result.Succeeded)
                {
                    return new OkObjectResult(new { Message = "Login successful", SignInResult = result });
                }
                else
                {
                    return new BadRequestObjectResult(new {Message = "Wrong password" , SignInResult = result});
                }
            }
            else
            {
                return new BadRequestObjectResult(new { Message = "Mail has not confirmed." });
            }
                
        }

        public override async Task<ObjectResult> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        
        public override async Task<ObjectResult> RegisterAsync(UserRegisterDto userRegistered )
        {
            var user = userRegistered.UserRegisterToUser();
            IdentityResult result = await _userManager.CreateAsync(user, userRegistered.Password);
            if (result.Succeeded)
            {
                _mailService.CreateMailConfirmationCode(user);
                return new OkObjectResult(new { Message = "Profile creation is successful", IdentityResult = result });
            }
            else
            {
                return new BadRequestObjectResult(new { Message = "Profile creation is not successful", IdentityResult = result });
            }
                     

                         
        }

        public override Task<ObjectResult> SearchUserAsync(string name)
        {
            throw new NotImplementedException();
        }




    }
}
