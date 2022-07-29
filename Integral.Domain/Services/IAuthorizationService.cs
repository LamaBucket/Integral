using Integral.Domain.Models.Enums;

namespace Integral.Domain.Services
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResult> Authorize(string username, Role userRole);
    }
}
