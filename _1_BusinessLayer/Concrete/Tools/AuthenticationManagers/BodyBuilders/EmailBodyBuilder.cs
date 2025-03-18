using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.BodyBuilders
{
    public class EmailBodyBuilder
    {
        public (string body, string subject) BuildChangeEmailBody(string token)
        {
            var body = $"To confirm your email change, please use the following token: {token}";
            var subject = "Confirm Your Email Change";
            return (body, subject);
        }

        public (string body, string subject) BuildChangePhoneNumberBody(string token)
        {
            var body = $"To confirm your phone number change, please use the following token: {token}";
            var subject = "Confirm Your Phone Number Change";
            return (body, subject);
        }

        public (string body, string subject) BuildMailConfirmationBody(string token)
        {
            var body = $"Please confirm your email by using the following token: {token}";
            var subject = "Confirm Your Email Address";
            return (body, subject);
        }

        public (string body, string subject) BuildPasswordResetBody(string token)
        {
            var body = $"To reset your password, please use the following token: {token}";
            var subject = "Reset Your Password";
            return (body, subject);
        }

        public (string body, string subject) BuildTwoFactorBody(string token)
        {
            var body = $"Your two factor authentication code: {token}";
            var subject = "Two Factor";
            return (body, subject);
        }
    }
}
