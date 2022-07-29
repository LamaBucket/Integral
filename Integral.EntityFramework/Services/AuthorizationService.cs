using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;

namespace Integral.EntityFramework.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private IUserDataService _usersDataService;

        public AuthorizationService(IUserDataService usersDataService)
        {
            _usersDataService = usersDataService;
        }

        public async Task<AuthorizationResult> Authorize(string username, Role userRole)
        {
            User? user = await _usersDataService.Get(username);

            if (user is null)
                return AuthorizationResult.IncorrectUsername;

            if (user?.UserRoles?.Select(x => x.Role).Contains(userRole) ?? false)
                return AuthorizationResult.Ok;

            return AuthorizationResult.IncorrectRole;
        }
    }
}
