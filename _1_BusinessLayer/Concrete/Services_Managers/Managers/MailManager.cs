using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace _1_BusinessLayer.Concrete.Services.SideServices
{
    public class MailManager : AbstractMailManager
    {
        public MailManager(AbstractUserRepository userRepository) : base(userRepository)
        {
        }

        public override async Task CreateMailConfirmationCodeAsync(User user)
        {

            var client = new SmtpClient();
            try
            {
                string mail = user.Email;
                Random random = new Random();
                int code = random.Next(100000, 1000000);

                MailboxAddress mailboxAddressFrom = new MailboxAddress("AiForum", "mihrsohbet@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", mail);

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Your confirmation code : " + code;

                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(mailboxAddressFrom);
                mimeMessage.To.Add(mailboxAddressTo);
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Ai forum confirmation code";


                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("mihrsohbet@gmail.com", "oskthruciuzphigk");
                await client.SendAsync(mimeMessage);
                user.ConfirmationCode = code;
                await _userRepository.UpdateAsync(user);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }           
        }
    }
}
