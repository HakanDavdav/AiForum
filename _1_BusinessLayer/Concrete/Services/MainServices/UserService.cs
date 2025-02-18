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

        public override async Task<ObjectResult> ChangePassword(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectResult> ConfirmEmail(int code, int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                if (_authenticationService.CheckMail(user, id))
                {
                    return new OkObjectResult(new { Message = "Mail is confirmed", boolean = _authenticationService.CheckMail(user, id) });
                }
                else
                {
                    return new BadRequestObjectResult(new { Message = "Mail is not confirmed", boolean = _authenticationService.CheckMail(user, id) });
                }
                
            }
            
        }

        public override async Task<ObjectResult> DeleteProfile(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                var result = _userManager.DeleteAsync(user);
                return new OkObjectResult(new { Message = "Deletion succesfull", IdentityResult = result });
            }
          
        }

        public override async Task<ObjectResult> EditProfile(int id, UserProfileDto userProfileDto)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                user.UserName = userProfileDto.username;
                user.imageUrl = userProfileDto.imageUrl;
                user.city = userProfileDto.city;
                return new OkObjectResult(new { Message = "Profile updated", UserProfileDto = user.UserToUserProfile() });
            }
           
        }

        public override async Task<ObjectResult> getByName(string name)
        {
            var user = _userRepository.GetByName(name);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }
            else
            {
                return new OkObjectResult(new { Message = "User found by name", UserProfileDto = user.UserToUserProfile() });
            }
      
        }

        public override async Task<ObjectResult> Login(UserLoginDto userLogged)
        {
            
            var user = _userRepository.GetByEmail(userLogged.emailOrUsername)??_userRepository.GetByUsername(userLogged.emailOrUsername);
            if (_authenticationService.CheckMail(user))
            {
                var result = _signInManager.PasswordSignInAsync(userLogged.emailOrUsername, userLogged.password, false, false);
                if (result.IsCompletedSuccessfully)
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

        public override async Task<ObjectResult> Logout()
        {
            throw new NotImplementedException();
        }

        
        public override async Task<ObjectResult> Register(UserRegisterDto userRegistered )
        {
            var user = new User();
            var result = _userManager.CreateAsync(user, userRegistered.password);
            if (result.IsCompletedSuccessfully)
            {
                _mailService.CreateMailConfirmationCode(user);
                return new OkObjectResult(new { Message = "Profile creation is successful", IdentityResult = result });
            }
            else
            {
                return new BadRequestObjectResult(new { Message = "Profile creation is not successful", IdentityResult = result });
            }
           
            

                         
        }

 




        //T methods//

        public override void TDelete(User t)
        {
            _userRepository.Delete(t);
        }

        public override List<User> TGetAll()
        {
            return _userRepository.GetAll();
        }

        public override User TGetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public override void TInsert(User t)
        {
            _userRepository.Insert(t);
        }

        public override void TUpdate(User t)
        {
            _userRepository.Update(t);
        }
    }
}
