using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.AuthIntegrations.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Factories;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using MailKit;
using Microsoft.AspNetCore.Identity;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;

namespace _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Senders
{
    public class MailSender
    {
        private readonly MailBodyBuilder _mailBodyBuilder;
        public MailSender(MailBodyBuilder mailBodyBuilder)
        {
            _mailBodyBuilder = mailBodyBuilder;
        }
        public async Task<IdentityResult> SendAuthenticationMailAsync(User user, string token, MailType? mailType)
        {
            SmtpClient smtpClient = null;
            try
            {
                var (body, subject) = _mailBodyBuilder.BuildAuthenticationMailContent(mailType, token);
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("mihrsohbet@gmail.com", "jkyp vxwy gicc cohs");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("mihrsohbet@gmail.com", user.Email, subject, body);
                    mailMessage.IsBodyHtml = true;

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex) when (ex is TimeoutException || ex is ServiceNotAuthenticatedException || ex is SmtpException)
            {
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }

        public async Task<IdentityResult> SendSocialMailAsync(User? FromUser, Bot? FromBot, User toUser, MailType type, string additionalInfo, int additionalId)
        {
            var (body, subject) = _mailBodyBuilder.BuildSocialMailContent(FromUser, FromBot, type, additionalInfo, additionalId);
            SmtpClient smtpClient = null;
            try
            {
                using (smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // TLS portu
                    smtpClient.Credentials = new NetworkCredential("mihrsohbet@gmail.com", "jkyp vxwy gicc cohs");
                    smtpClient.EnableSsl = true;

                    // E-posta mesajını oluşturma
                    var mailMessage = new MailMessage("mihrsohbet@gmail.com", toUser.Email, subject, body);
                    mailMessage.IsBodyHtml = true;

                    // E-posta gönderme işlemi
                    await smtpClient.SendMailAsync(mailMessage);
                    return IdentityResult.Success;

                }
            }
            catch (Exception ex) when (ex is TimeoutException || ex is ServiceNotAuthenticatedException || ex is SmtpException)
            {
                throw;
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }
    }
}
