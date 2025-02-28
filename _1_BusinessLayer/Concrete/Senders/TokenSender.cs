using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Bodybuilders;
using _1_BusinessLayer.Concrete.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Senders
{
    public class TokenSender : AbstractTokenSender
    {


        public TokenSender(AbstractUserRepository userRepository, AbstractTokenFactory tokenFactory,
            EmailBodyBuilder emailBodyBuilder, SmsBodyBuilder smsBodyBuilder) :
            base(userRepository, tokenFactory, emailBodyBuilder, smsBodyBuilder)
        {
        }

        public override async Task<IdentityResult> SendEmail_EmailConfirmationTokenAsync(User user)
        {
            SmtpClient smtpClient = null;
            try
            {
                var token = await _tokenFactory.CreateMailConfirmationTokenAsync(user);
                var (body, subject) = _emailBodyBuilder.BuildMailConfirmationBody(token);
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("your-email@gmail.com", user.Email, subject, body);

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new InternalServerError("Mail connection error"));
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }

        public override async Task<IdentityResult> SendEmail_EmailChangeTokenAsync(User user, string newMail)
        {
            SmtpClient smtpClient = null;
            try
            {
                var token = await _tokenFactory.CreateChangeEmailTokenAsync(user,newMail);
                var (body, subject) = _emailBodyBuilder.BuildChangeEmailBody(token);
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("your-email@gmail.com", newMail, subject, body);

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new InternalServerError("Mail connection error"));
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }

        public override async Task<IdentityResult> SendEmail_ResetPasswordTokenAsync(User user)
        {
            SmtpClient smtpClient = null;
            try
            {
                var token = await _tokenFactory.CreatePasswordResetTokenAsync(user);
                var (body, subject) = _emailBodyBuilder.BuildPasswordResetBody(token);
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("your-email@gmail.com", user.Email, subject, body);

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new InternalServerError("Mail connection error"));
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }


        public override async Task<IdentityResult> SendEmail_TwoFactorTokenAsync(User user)
        {
            SmtpClient smtpClient = null;
            try
            {
                var token = await _tokenFactory.CreateTwoFactorTokenAsync(user);
                var (body, subject) = _emailBodyBuilder.BuildTwoFactorBody(token);
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("your-email@gmail.com", user.Email, subject, body);

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new InternalServerError("Mail connection error"));
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }



        public override async Task<IdentityResult> SendSms_EmailConfirmationTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> SendSms_EmailChangeTokenAsync(User user, string newMail)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> SendSms_ResetPasswordTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> SendSms_TwoFactorTokenAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
