using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;

namespace _1_BusinessLayer.Concrete.Tools.BodyBuilders
{
    public class MailBodyBuilder
    {


        public (string body, string subject) BuildAuthenticationMailContent(
                MailType? type,
                string token)
        {
            string subject = string.Empty;
            string body = string.Empty;

            switch (type)
            {
                case MailType.ChangeEmail:
                    subject = "Confirm Your Email Change";
                    body = $"To confirm your email change, please use the following token: {token}";
                    break;

                case MailType.ConfirmEmail:
                    subject = "Confirm Your Email Address";
                    body = $"Please confirm your email by using the following token: {token}";
                    break;

                case MailType.ResetPassword:
                    subject = "Reset Your Password";
                    body = $"To reset your password, please use the following token: {token}";
                    break;

                case MailType.TwoFactor:
                    subject = "Two Factor Authentication";
                    body = $"Your two factor authentication code: {token}";
                    break;

                default:
                    subject = "Authentication Notification";
                    body = token;
                    break;
            }

            return (body, subject);
        }


        public (string body, string subject) BuildSocialMailContent(User? FromUser, Bot? FromBot, MailType?
            type, string additionalInfo, int additionalId)
        {
            string senderName = FromUser?.UserName ?? FromBot?.BotProfileName ?? "";
            string subject = type switch
            {
                MailType
                .EntryLike => $"{senderName} liked your entry",
                MailType
                .PostLike => $"{senderName} liked your post",
                MailType
                .CreatingEntry => $"{senderName} created a new entry.",
                MailType
                .CreatingPost => $"{senderName} created a new post.",
                MailType
                .GainedFollower => $"{senderName} started following you.",
                MailType
                .Message => $"{senderName} sent you a message.",
                MailType
                .BotActivity => $"{senderName} has new bot activity.",
                MailType
                .NewEntryForPost => $"{senderName} added a new entry to your post",
                _ => ""
            };

            string body = type switch
            {
                MailType
                .EntryLike => $"{additionalInfo} entry <br/><a href='https://example.com/entry/{additionalId}'>View Entry</a>",
                MailType
                .PostLike => $"{additionalInfo} post <br/><a href='https://example.com/post/{additionalId}'>View Post</a>",
                MailType
                .CreatingEntry => $"{additionalInfo} entry <br/><a href='https://example.com/entry/{additionalId}'>View Entry</a>",
                MailType
                .CreatingPost => $"{additionalInfo} post <br/><a href='https://example.com/post/{additionalId}'>View Post</a>",
                MailType
                .GainedFollower => $"{additionalInfo} user <br/><a href='https://example.com/post/{additionalId}'>View OwnerUser</a>",
                MailType
                .Message => $"{additionalInfo} user <br/><a href='https://example.com/post/{additionalId}'>View OwnerUser</a>",
                MailType
                .BotActivity => $"{additionalInfo} bot <br/><a href='https://example.com/post/{additionalId}'>View OwnerBot</a>",
                MailType
                .NewEntryForPost => $"{additionalInfo} entry <br/><a href='https://example.com/post/{additionalId}'>View Entry</a>",
                _ => ""
            };

            return (body, subject);
        }


    }
}
