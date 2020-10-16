using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Requests;
using Sabio.Models.Requests.UserProfiles;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        public IUserService _service = null;
        public IEmailService _emailService = null;
        public IAuthenticationService<int> _authService = null;
        public UserApiController(IUserService service
            , ILogger<UserApiController> logger
            , IAuthenticationService<int> authService, IEmailService emailService) : base(logger)
        {
            _service = service;
            _authService = authService;
            _emailService = emailService; 
        }
        [HttpPost("verify"), AllowAnonymous]
        public ActionResult<SuccessResponse> VerifyEmail(string email)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                bool verifiedEmail = _service.VerifyEmail(email);
                

                if (verifiedEmail == true)
                {
                    TokenAddRequest model = new TokenAddRequest();
                    model.Email = email;
                    model.Token =  Guid.NewGuid();
                    model.TokenType = 2;
                    _service.InsertToken(model);
                    _emailService.ForgotPassEmail(model);
                    //send email 
                    response = new SuccessResponse();

                }
                else
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }            
                 

     
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
       
        }

        [HttpPost("reset"), AllowAnonymous]
        public ActionResult<SuccessResponse> ResetPass(ResetPassAddRequest model)
        {
            int code = 201;
            BaseResponse response = null;
            try
            {
                //callsvc
                _service.ResetPass(model);
                response = new SuccessResponse();
                
            }
            catch (Exception ex)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
                code = 500;
                response = new ErrorResponse(ex.Message);

            }
            return StatusCode(code, response);
        }

        [HttpPut("{id:int}/status/{statusId:int}")]
        public ActionResult<ItemResponse<int>> UserStatusUpdate(int id, int statusId)
		{
            int code = 200;
            BaseResponse response = null;
            try
			{
                _service.UserStatusUpdate(id, statusId);

                response = new SuccessResponse();
			}
            catch(Exception exception)
			{
                code = 500;
                response = new ErrorResponse(exception.Message);
			}
            return StatusCode(code, response);
		}

    }
}
