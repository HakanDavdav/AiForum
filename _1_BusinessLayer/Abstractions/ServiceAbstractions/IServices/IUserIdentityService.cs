using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IUserIdentityService
    {
        Task<IdentityResult> ChangeEmail(int userId,string newEmail ,string changeEmailToken);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername);
        Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken);
        Task<IdentityResult> SetPhoneNumber(int userId, string newPhoneNumber);
        Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider);
        Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto);
        Task<IdentityResult> Logout();
        Task<IdentityResult> ChooseProviderAndSendToken(int userId, string provider, string operation, string newEmail, string newPhoneNumber);
        Task<IdentityResult> ChooseProviderAndSendToken(string provider, string operation, string usernameOrEmailOrPhoneNumber);
        Task<IdentityResult> PasswordReset(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword);
        Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
        Task<IdentityResult> ActivateTwoFactorAuthentication(int userId);
        Task<IdentityResult> DisableTwoFactorAuthentication(int userId);
        Task<IdentityResult> ConfirmEmail(string emailConfirmationToken, string usernameEmailOrPhoneNumber);



    }
}