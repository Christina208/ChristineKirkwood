using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models.AppSettings;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailAPIController : BaseApiController
    {
        private IEmailService _emailService = null;
        private AppKeys _appKeys;

        public EmailAPIController(IEmailService emailServices, IOptions<AppKeys> appKeys, ILogger<EmailAPIController> logger) : base(logger)
        {
            _emailService = emailServices;
            _appKeys = appKeys.Value;

        }


        [HttpPost("contactus"), AllowAnonymous]
        public async Task<ActionResult<SuccessResponse>> ContactUsAsync(ContactUsRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                await _emailService.ContactUs(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
       
    }
}
