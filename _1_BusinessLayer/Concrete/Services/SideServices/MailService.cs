using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace _1_BusinessLayer.Concrete.Services.SideServices
{
    public class MailService : AbstractMailService
    {
        public MailService(AbstractUserRepository userRepository) : base(userRepository)
        {
        }

        public override void CreateMailConfirmationCode(User user)
        {
            string mail = user.Email;
            Random random = new Random();
            int code = random.Next(100000,1000000);
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("AiForum", "mihrsohbet@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User",mail);
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Your confirmation code : " + code;          
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Ai forum confirmation code";
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("mihrsohbet@gmail.com", "oskthruciuzphigk");
            client.Send(mimeMessage);
            client.Disconnect(true);
            user.confirmationCode = code;
            _userRepository.Update(user);
            

        }
    }
}
