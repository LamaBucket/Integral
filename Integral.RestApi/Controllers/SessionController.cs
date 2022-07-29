using Integral.Domain.Models.Enums;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthorizationResult = Integral.Domain.Models.Enums.AuthorizationResult;
using IAuthenticationService = Integral.Domain.Services.IAuthenticationService;
using IAuthorizationService = Integral.Domain.Services.IAuthorizationService;

namespace Integral.RestApi.Controllers
{

    [Route("[controller]")]
    public class SessionController : Controller
    {
        private IAuthenticationService _authenticationService;
        private IAuthorizationService _authorizationService;

        public SessionController(IAuthenticationService authenticationService, IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(string username, string password, Role userRole)
        {
            await HttpContext.SignOutAsync();

            AuthenticationResult result = await _authenticationService.Login(username, password);
            AuthorizationResult result2 = await _authorizationService.Authorize(username, userRole);

            int errorCode = -999;

            if (result == AuthenticationResult.Ok)
            {
                if (result2 == AuthorizationResult.Ok)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, userRole.ToString()) };

                    ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(claimsIdentity));

                    return Ok();
                }

                switch (result2)
                {
                    case AuthorizationResult.IncorrectUsername:
                        return StatusCode(500);
                    case AuthorizationResult.IncorrectRole:
                        errorCode = ApiErrorCodes.LoginIncorrectRole;
                        break;
                }

            }

            switch (result)
            {
                case AuthenticationResult.IncorrectUsername:
                    errorCode = ApiErrorCodes.LoginIncorrectUsername;
                    break;
                case AuthenticationResult.IncorrectPassword:
                    errorCode = ApiErrorCodes.LoginIncorrectPassword;
                    break;
            }

            return BadRequest(errorCode);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Logoff()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }
    }
}
