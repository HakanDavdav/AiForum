using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
{
    public interface IUserIdentityService
    {
        Task<IdentityResult> ConfirmChangeEmailToken(int userId,string newEmail ,string changeEmailToken);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IdentityResult> ChangeUsername(int userId, string oldUsername, string newUsername);
        Task<IdentityResult> ConfirmPhoneNumberConfirmationToken(int userId, string phoneConfirmationToken);
        Task<IdentityResult> SetUnconfirmedPhoneNumber(int userId, string newPhoneNumber);
        Task<IdentityResult> LoginTwoFactor(UserLoginDto userLoginDto, string twoFactorToken, string provider);
        Task<IdentityResult> LoginDefault(UserLoginDto userLoginDto);
        Task<IdentityResult> Logout();
        Task<IdentityResult> ChooseProviderAndSendToken(int userId, string provider, MailTypes.MailType? mailType, SmsTypes.SmsType? smsType, string newEmail, string newPhoneNumber, string newPassword);
        Task<IdentityResult> ConfirmPasswordResetToken(string usernameOrEmailOrPhoneNumber, string resetPasswordToken, string newPassword);
        Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
        Task<IdentityResult> ActivateTwoFactorAuthentication(int userId);
        Task<IdentityResult> DisableTwoFactorAuthentication(int userId);
        Task<IdentityResult> ConfirmEmailConfirmationToken(string emailConfirmationToken, string usernameEmailOrPhoneNumber);



    }
}