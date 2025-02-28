using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IUserService
    {
        Task<IdentityResult> ChangeEmail(int userId,string newEmail ,string changeEmailToken);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IdentityResult> ConfirmEmail(int userId, string confirmEmailToken);
        Task<IdentityResult> ConfirmPhoneNumber(int userId, string phoneConfirmationToken);
        Task<IdentityResult> Login(UserLoginDto userLoginDto, string twoFactorToken);
        Task<IdentityResult> Logout();
        Task<IdentityResult> PasswordReset(int userId, string newPassword, string resetPasswordToken);
        Task<IdentityResult> Register(UserRegisterDto userRegisterDto);
    }
}