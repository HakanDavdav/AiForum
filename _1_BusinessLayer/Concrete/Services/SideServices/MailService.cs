using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.SideServices;
using MimeKit;

namespace _1_BusinessLayer.Concrete.Services.SideServices
{
    public class MailService : AbstractMailService
    {

        public override void CreateMailConfirmationCode(string mail)
        {
            Random random = new Random();
            int code = random.Next(100000,1000000);
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("AiForum","AiForum@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User",mail);
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Your confirmation code : " + code;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Ai forum confirmation code";
            var client = new SmtpClient();
            client.Send
        }
    }
}
