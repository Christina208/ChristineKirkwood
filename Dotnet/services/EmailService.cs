using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sabio.Models.AppSettings;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class EmailService : IEmailService
    {
        private IWebHostEnvironment _env;
        private IConfiguration _config;
        private AppKeys _appKeys;
       
        public EmailService(IOptions<AppKeys> appKeys, IWebHostEnvironment env, IConfiguration config)/*pss in instance of dataprovider*/
        {
            _appKeys = appKeys.Value;
            _env = env;
            _config = config;
          
        }

        private async Task Send(SendGridMessage msg)
        {
            SendGridClient client = new SendGridClient(_appKeys.SendGridApiKey);
            var response = await client.SendEmailAsync(msg);
            var test = response;
        }

        public async Task ContactUs(ContactUsRequest model)
        {
            /* string htmlContentPath = _env.WebRootPath + "/EmailTemplates/ContactUs.html";
             string htmlContent = System.IO.File.ReadAllText(htmlContentPath).Replace("{{&message}}", model.Message).Replace("{{&toName}}", model.To);*/
            SendGridMessage message = new SendGridMessage()
            {
                /*From = new EmailAddress("brijesh@sabio.la", "Example User"),*/
                From = new EmailAddress(model.From, model.Name),
                Subject = model.Subject,
                HtmlContent = model.Message,
            };

            message.AddTo("christine007@dispostable.com");
            await Send(message);
        }
        public async Task ForgotPassEmail(TokenAddRequest model)
        {
            string domain = _config.GetSection("Domain").Value;
            string htmlContentPath = _env.WebRootPath + "/EmailTemplates/ForgotPass.html";
            string htmlContent = System.IO.File.ReadAllText(htmlContentPath).Replace("{&token}", model.Token.ToString()).Replace("{&domain}", domain);
            SendGridMessage message = new SendGridMessage()
            {
                From = new EmailAddress("brijesh@sabio.la"),
                Subject = "Reset your Machitia password",
                HtmlContent = htmlContent,
            };

            message.AddTo(model.Email);
            await Send(message);
        }

    }
}
