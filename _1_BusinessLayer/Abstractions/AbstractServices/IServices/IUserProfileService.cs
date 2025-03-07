using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IUserProfileService
    {
        Task<IdentityResult> EditPreferences (int userId,UserEditPreferencesDto userEditPreferencesDto);
        Task<IdentityResult> EditProfile (int userId,UserEditProfileDto userEditProfileDto);
        Task<IdentityResult> CreateProfile (int userId,UserCreateProfileDto userCreateProfileDto);
        Task<ObjectIdentityResult<User>> GetUserProfile(int userId);
        Task<ObjectIdentityResult<List<Notification>>> GetNotifications(int userId);
    }
}
