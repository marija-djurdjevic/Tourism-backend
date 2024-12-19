using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Public;

namespace Explorer.Blog.Core.UseCases
{
    public class MailService : IMailService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("tutorialseu-dev@outlook.com", "Test12345678"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true, 
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
