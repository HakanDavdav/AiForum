using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IUserService
    {
        Task<IdentityResult> ChangeEmail(int userId,string newEmail ,string changeEmailToken);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername);
        Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken);
        Task<IdentityResult> Login(UserLoginDto userLoginDto, string twoFactorToken);
        Task<IdentityResult> Logout();
        Task<IdentityResult> PasswordReset(int userId, string newPassword, string resetPasswordToken);
        Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
        Task<IdentityResult> ActivateTwoFactorAuthentication(int userId);
        Task<IdentityResult> DisableTwoFactorAuthentication(int userId);
        Task<IdentityResult> ConfirmEmail(UserLoginDto userLoginDto, string emailConfirmationToken);
        Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto);
        Task<IdentityResult> EditPreferences(int userId, UserEditPreferencesDto userPreferencesDto);



    }
}