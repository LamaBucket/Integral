using Integral.Domain.Models.Enums;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Integral.RestApi;
using System.Security.Claims;
using AuthorizationResult = Integral.Domain.Models.Enums.AuthorizationResult;
using IAuthenticationService = Integral.Domain.Services.IAuthenticationService;
using IAuthorizationService = Integral.Domain.Services.IAuthorizationService;
using Integral.Domain.Services;
using Integral.Domain.Models;

namespace Integral.RestApi.Controllers
{

    [Route("[controller]")]
    public class SessionController : Controller
    {
        private IAuthenticationService _authenticationService;
        private IAuthorizationService _authorizationService;
        private IUserDataService _userDataService;

        public SessionController(IAuthenticationService authenticationService, IAuthorizationService authorizationService, IUserDataService userDataService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userDataService = userDataService;
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

                    return Json(await _userDataService.Get(username));
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
