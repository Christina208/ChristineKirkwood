using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/admindash")]
    [ApiController]
    public class AdminDashApiController : BaseApiController
    {
        public IAdminDashServices _service = null;
        public IAuthenticationService<int> _authService = null;
        public AdminDashApiController(IAdminDashServices service, ILogger<AdminDashApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;

        }
        [HttpGet]
        public ActionResult<ItemsResponse<List<AdminDash>>> Get()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDash> list = _service.Get();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<AdminDash> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }


            return StatusCode(code, response);

        }



    }
}
