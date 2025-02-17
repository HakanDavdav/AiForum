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

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class UserService : AbstractUserService 
    {
        public UserService(AbstractAuthenticationService authenticationService, 
            AbstractMailService mailService, AbstractUserRepository userRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager) : base(authenticationService, mailService, userRepository, userManager, signInManager)
        {
        }

        public override async Task<IdentityResult> ChangePassword(int id)
        {
            throw new NotImplementedException();
        }

        public override bool ConfirmEmail(int code, int id)
        {
            var user = _userRepository.GetById(id);
            return _authenticationService.CheckMail(user, id);
        }

        public override async Task<IdentityResult> DeleteProfile(int id)
        {
            var user = _userRepository.GetById(id);
            var result = _userManager.DeleteAsync(user);
            return await result;
        }

        public override async Task<Boolean> EditProfile(int id, UserProfileDto userProfileDto)
        {
            var user = _userRepository.GetById(id);
            user.UserName = userProfileDto.username;
            user.imageUrl = userProfileDto.imageUrl;
            user.city = userProfileDto.city;
            return true;
        }

        public override async Task<User> getByName(string name)
        {
            throw new NotImplementedException();
        }

        public override async Task<SignInResult> Login(UserLoginDto userLogged)
        {
            
            var user = _userRepository.GetByEmail(userLogged.email);
            if (_authenticationService.CheckMail(user))
            {
                var result = _signInManager.PasswordSignInAsync(userLogged.email, userLogged.password, false, false);
                return await result;
            }
            else
            {
                SignInResult result = new SignInResult();
                return SignInResult.Failed;
            }
                
        }

        public override async Task<IdentityResult> Logout()
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> Register(UserRegisterDto userRegistered )
        {
            var user = new User();
            var result = _userManager.CreateAsync(user, userRegistered.password);
            _mailService.CreateMailConfirmationCode(user);
            return await result;

                         
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
