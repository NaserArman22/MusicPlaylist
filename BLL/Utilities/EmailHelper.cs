using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public class EmailHelper
    {
        
        public static void ShareViaEmail(string email, string subject, string body)
        {
            // Configure the SMTP client
            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 2525, // Typically 587 for secure connections
                Credentials = new NetworkCredential("4309f7e10a62b6", "01c9006ac41212"),
                EnableSsl = true,
            };

            // Configure the mail message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("bakor99784@maonyn.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false, // Change to true if you want HTML emails
            };

            mailMessage.To.Add(email); // Add the recipient email

            // Send the email
            smtpClient.Send(mailMessage);
           
        }
    }
}
