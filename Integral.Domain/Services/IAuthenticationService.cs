using Integral.Domain.Models.Enums;

namespace Integral.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Login(string username, string password);
    }
}
