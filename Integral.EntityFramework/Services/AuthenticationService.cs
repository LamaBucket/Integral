using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;

namespace Integral.EntityFramework.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private IUserDataService _userDataService;
        private IPasswordHashService _passwordHasher;

        public AuthenticationService(IUserDataService userDataService, IPasswordHashService passwordHasher)
        {
            _userDataService = userDataService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticationResult> Login(string username, string password)
        {
            User? user = await _userDataService.Get(username);

            if (user is null)
                return AuthenticationResult.IncorrectUsername;


            if (!_passwordHasher.VerifyPassword(user.PasswordHash, password))
                return AuthenticationResult.IncorrectPassword;


            return AuthenticationResult.Ok;
        }
    }
}
