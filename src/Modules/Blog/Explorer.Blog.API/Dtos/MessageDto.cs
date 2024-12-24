using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class MessageDto
    {
        public string ToEmail { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public MessageDto(string toEmail, string fromEmail, string password, string subject, string body) {
            ToEmail = toEmail;
            FromEmail = fromEmail;
            Password = password;
            Subject = subject;
            Body = body;
        }
    }
}
