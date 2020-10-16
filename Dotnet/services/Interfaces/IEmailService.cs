using Sabio.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IEmailService
    {
        Task ContactUs(ContactUsRequest model);
        Task ForgotPassEmail(TokenAddRequest model);
    }
}
