using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IMailService
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}
