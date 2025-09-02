using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Factories;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;
using static _2_DataAccessLayer.Concrete.Enums.SmsTypes;

namespace _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Senders
{
    public class GeneralSender
    {
        MailSender _mailSender;
        SmsSender _smsSender;
        NotificationSender _notificationSender;
        TokenFactory _tokenFactory;
        AbstractUserRepository _userRepository;
        AbstractBotRepository _botRepository;
        public GeneralSender(MailSender mailSender, SmsSender smsSender, NotificationSender sender, 
            TokenFactory tokenFactory, AbstractUserRepository userRepository, AbstractBotRepository botRepository)
        {
            _mailSender = mailSender;
            _smsSender = smsSender;
            _notificationSender = sender;
            _tokenFactory = tokenFactory;
            _userRepository = userRepository;
            _botRepository = botRepository;
        }

        public async Task<IdentityResult> GeneralAuthenticationSend(User user, MailType? mailType, SmsType? smsType, string? newPhoneNumber, string? newEmail, string? newPassword)
        {
            var token = string.Empty;
            if (mailType != null)
            {
                switch (mailType)
                {
                    case MailType.ChangeEmail:
                        token = await _tokenFactory.CreateChangeEmailTokenAsync(user, newEmail);
                        await _mailSender.SendAuthenticationMailAsync(user, token, mailType);
                        break;

                    case MailType.ConfirmEmail:
                        token = await _tokenFactory.CreateMailConfirmationTokenAsync(user);
                        await _mailSender.SendAuthenticationMailAsync(user, token, mailType);
                        break;

                    case MailType.ResetPassword:
                        token = await _tokenFactory.CreatePasswordResetTokenAsync(user);
                        await _mailSender.SendAuthenticationMailAsync(user, token, mailType);
                        break;

                    case MailType.TwoFactor:
                        token = await _tokenFactory.CreateTwoFactorTokenAsync(user, "Email");
                        await _mailSender.SendAuthenticationMailAsync(user, token, mailType);
                        break;

                    default:
                        return IdentityResult.Failed(new IdentityError { Description = "Invalid mail type." });
                }
            }
            else if (smsType != null)
            {
                switch (smsType)
                {
                    case SmsType.ConfirmPhoneNumber:
                        token = await _tokenFactory.CreateConfirmPhoneNumberTokenAsync(user, newPhoneNumber);
                        await _smsSender.SendAuthenticationSms(user, token, smsType);
                        break;

                    case SmsType.TwoFactor:
                        token = await _tokenFactory.CreateTwoFactorTokenAsync(user, "Phone");
                        await _smsSender.SendAuthenticationSms(user, token, smsType);
                        break;

                    case SmsType.ChangePhoneNumber:
                        token = await _tokenFactory.CreateConfirmPhoneNumberTokenAsync(user, user.PhoneNumber);
                        await _smsSender.SendAuthenticationSms(user, token, smsType);
                        break;

                    case SmsType.ResetPassword:
                        token = await _tokenFactory.CreatePasswordResetTokenAsync(user);
                        await _smsSender.SendAuthenticationSms(user, token, smsType);
                        break;

                    default:
                        return IdentityResult.Failed(new IdentityError { Description = "Invalid SMS type." });
                }
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> GeneralSocialSend(MailEvent? mailEvent, NotificationEvent? notificationEvent)
        {
            if (mailEvent != null)
            {
                var toUser = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Id==mailEvent.ReceiverUserId).Include(user => user.UserPreference);
                var fromBot = await _botRepository.GetByIdAsync(mailEvent.SenderBotId);
                var fromUser = await _userRepository.GetByIdAsync(mailEvent.SenderUserId);
                if (toUser != null )
                {
                    if (toUser.EmailConfirmed == true && toUser.UserPreference.SocialEmailPreference == true)
                    {
                        await _mailSender.SendSocialMailAsync(fromUser, fromBot, toUser, mailEvent.Type, mailEvent.AdditionalInfo, mailEvent.AdditionalId);
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("OwnerUser has disabled social emails or email is not confirmed"));
                }
                return IdentityResult.Failed(new NotFoundError("Receiver user not found"));
            }
            if (notificationEvent != null)
            {
                var toUser = await _userRepository.GetByIdAsync(notificationEvent.ReceiverUserId);
                var fromBot = await _botRepository.GetByIdAsync(notificationEvent.SenderBotId);
                var fromUser = await _userRepository.GetByIdAsync(notificationEvent.SenderUserId);
                if (toUser != null)
                {
                    if (toUser.UserPreference.SocialNotificationPreference == true)
                    {
                        await _notificationSender.SendSocialNotificationAsync(fromUser, fromBot, toUser, notificationEvent.Type, notificationEvent.AdditionalInfo, notificationEvent.AdditionalId);
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("OwnerUser has disabled social notifications"));
                }
                return IdentityResult.Failed(new NotFoundError("Receiver user not found"));
            }
            return IdentityResult.Failed(new UnexpectedError("Both events are null"));
        }

    }
}
