using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Dtos;

namespace Explorer.Blog.Core.UseCases
{
    public class MailService : IMailService
    {
        public async Task SendEmail(MessageDto message)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(message.FromEmail, message.Password),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(message.FromEmail),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(message.ToEmail);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
