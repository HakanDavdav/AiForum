﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Errors;
using _1_BusinessLayer.Concrete.Mappers;
using _1_BusinessLayer.Concrete.Services.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class UserService : AbstractUserService 
    {
        public UserService(AbstractAuthenticationManager authenticationManager,
            AbstractMailManager mailManager, AbstractUserRepository userRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager) : base(authenticationManager, mailManager, userRepository, userManager, signInManager)
        {
        }

        public override async Task<IdentityResult> ChangeEmailAsync(int id, string email)
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                try
                {
                    await _mailManager.CreateMailConfirmationCodeAsync(user);
                }
                catch (Exception ex) 
                {
                    return IdentityResult.Failed(new InternalServerError("Exception in mail service"));
                }
                var changeEmailResult = await _userManager.ChangeEmailAsync(user, email, code.ToString());
            }
        }

        public override async Task<IdentityResult> ChangePasswordAsync(int id,string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (changePasswordResult.Succeeded)
                {
                    //Successfull
                    return changePasswordResult.ChangePasswordResultToIdentityResult();
                }
                else
                {
                    //Unsuccessfull
                    return changePasswordResult.ChangePasswordResultToIdentityResult();
                }
            }
        }

        public override Task<IdentityResult> ChangeUserPreferencesAsync(int id, UserPreferences userPreferences)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> ConfirmEmailAsync(int code, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
               
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                var result = await _authenticationManager.ValidateEmailAsync(user, code);
                if (result.Succeeded)
                {
                    
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new ValidationError("Confirmation code is wrong"));
                }
            }

        }

        public override async Task<IdentityResult> DeleteProfileAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                await _userManager.DeleteAsync(user);
                return IdentityResult.Success;
            }

        }

        public override async Task<IdentityResult> EditProfileAsync(int id, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                user.City = userEditProfileDto.City;
                user.ProfileName = userEditProfileDto.ProfileName;
                user.ImageUrl = userEditProfileDto.ImageUrl;
                await _userRepository.UpdateAsync(user);
                return IdentityResult.Success;
            }

        }

        public override async Task<IdentityResult> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                //User not found
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                return IdentityResult.Success;
            }

        }

        public override async Task<IdentityResult> LoginAsync(UserLoginDto userLogged)
        {
            var user = await _userRepository.GetByEmailAsync(userLogged.EmailOrUsername)??
                       await _userRepository.GetByUsernameAsync(userLogged.EmailOrUsername);
            if (user == null)
            {
                //User not found
                return IdentityResult.Failed(new NotFoundError("Invalid username or password"));
            }
            else
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user,userLogged.Password,false,false);
                if (signInResult.Succeeded&&_authenticationManager.CheckEmailValidation(user).Succeeded)
                {
                    //Successfull
                    return signInResult.SignInResultToIdentityResult();
                }
                else if(signInResult.Succeeded) 
                {
                    //Email not confirmed
                    //Passing _authenticationManager's errors
                    return signInResult.SignInResultToIdentityResult(_authenticationManager.CheckEmailValidation(user).Errors.ToList());
                }
                else
                {
                    //Invalid password or username
                    return signInResult.SignInResultToIdentityResult();
                }

            }

        }

        public override async Task<IdentityResult> LogoutAsync(int id)
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;
        }

        
        public override async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegistered )
        {
            var user = userRegistered.UserRegisterToUser();
            var createUserResult = await _userManager.CreateAsync(user,userRegistered.Password);
            if (createUserResult.Succeeded)
            {
                try
                {
                    await _mailManager.CreateMailConfirmationCodeAsync(user);
                }
                catch (Exception ex)
                {   
                    //If there is a Email connection exception do this;
                    return createUserResult.CreateUserResultToIdentityResult(new List<IdentityError> { new InternalServerError("Exception in mail service") });
                    throw;
                }
                //Successfull
                return createUserResult.CreateUserResultToIdentityResult();
            }
            else
            {
                //Unsuccessfull
                return createUserResult.CreateUserResultToIdentityResult();
            }

        }

        public override async Task<IdentityResult> SendEmailConfirmationCodeAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                //User not found
                return IdentityResult.Failed(new NotFoundError("User not found"));
            }
            else
            {
                try
                {
                    await _mailManager.CreateMailConfirmationCodeAsync(user);
                }
                catch(Exception ex)
                {
                    //If there is a Email connection exception do this;
                    return IdentityResult.Failed(new InternalServerError("Exception in mail service"));
                    throw;
                }
                //Succesfull
                return IdentityResult.Success;
            }
        }

        public override Task<IdentityResult> SearchUserAsync(string name)
        {
            throw new NotImplementedException();
        }




    }
}
