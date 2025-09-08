using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
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


        public (string body, string subject) BuildSocialMailContent(User? FromUser, Bot? FromBot, MailEvent mailEvent)
        {
            string profileName = FromUser?.UserName ?? FromBot?.BotProfileName ?? "";
            string title = mailEvent.Type switch
            {
                MailType
                .EntryLike => $"{profileName} liked your entry",
                MailType
                .PostLike => $"{profileName} liked your post",
                MailType
                .CreatingEntry => $"{profileName} created a new entry.",
                MailType
                .CreatingPost => $"{profileName} created a new post.",
                MailType
                .GainedFollower => $"{profileName} started following you.",          
                MailType
                .NewEntryForPost => $"{profileName} added a new entry to your post",
                _ => ""
            };

            string body = mailEvent.Type switch
            {
                MailType
                .EntryLike => $"{mailEvent.AdditionalInfo} entry <br/><a href='https://example.com/entry/{mailEvent.AdditionalId}'>View Entry</a>",
                MailType
                .PostLike => $"{mailEvent.AdditionalInfo} post <br/><a href='https://example.com/post/{mailEvent.AdditionalId}'>View Post</a>",
                MailType
                .CreatingEntry => $"{mailEvent.AdditionalInfo} entry <br/><a href='https://example.com/entry/{mailEvent.AdditionalId}'>View Entry</a>",
                MailType
                .CreatingPost => $"{mailEvent.AdditionalInfo} post <br/><a href='https://example.com/post/{mailEvent.AdditionalId}'>View Post</a>",
                MailType
                .GainedFollower => $"{mailEvent.AdditionalInfo} user <br/><a href='https://example.com/post/{mailEvent.AdditionalId}'>View Follower</a>",
                MailType
                .NewEntryForPost => $"{mailEvent.AdditionalInfo} entry <br/><a href='https://example.com/post/{mailEvent.AdditionalId}'>View Entry</a>",
                _ => ""
            };

            return (body, title);
        }


    }
}
