using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.AbstractTools.ITools
{
    public interface ITokenFactory
    {
        Task<string> CreateChangeEmailTokenAsync(User user, string newEmail);
        Task<string> CreateChangePhoneNumberTokenAsync(User user, string phoneNumber);
        Task<string> CreateMailConfirmationTokenAsync(User user);
        Task<string> CreatePasswordResetTokenAsync(User user);
        Task<string> CreateTwoFactorTokenAsync(User user);
    }
}