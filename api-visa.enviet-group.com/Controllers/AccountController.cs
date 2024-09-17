using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using EV.Model.Models.Request.Account;
using EV.Model.Models.Shared;
using EV.Model.Models.Interfaces;
using EV.Model.Models.Response.AccountResponse;
using API_EV.Areas.DaiLy.Models.Validations;
using SecuringWebApiUsingApiKey.Attributes;

namespace API_EV.Areas.DaiLy.Controllers
{
    [ApiKey]
    [Authorize]
    [Route("daily/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IConfiguration _configuration;
        IAuthenticateService _authenticateService;
        public AccountController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            IReturnObject returnObject = new ReturnObject();
            var validator = new AuthenticateValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                Task<AuthenticateResponse> account = _authenticateService.Authenticate(request.UserName, request.Password);
                string token = string.Empty;
                try
                {
                    returnObject.result = account.Result;
                    token = account.Result.GetToken();
                    account.Result.Token = token;
                }
                catch (Exception)
                {
                    AuthenticateResponse reponseAuthenticate = new AuthenticateResponse();
                    returnObject.status = HttpStatusCode.BadRequest;
                    returnObject.message = "Fail";
                    reponseAuthenticate.Message = "Incorrect Username or Password !";
                    returnObject.result = reponseAuthenticate;
                }

                Response.Headers.Add("Token", token);
            }
            else
            {
                AuthenticateResponse reponseAuthenticate = new AuthenticateResponse();
                returnObject.status = HttpStatusCode.BadRequest;
                returnObject.message = "Fail";
                reponseAuthenticate.Message = validationResult.Errors[0].ToString();
                returnObject.result = reponseAuthenticate;

            }

            return Ok(returnObject);
        }
    }
}
