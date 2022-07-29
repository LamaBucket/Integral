using Integral.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Integral.RestApi.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return String.Join("|", Encoding.UTF8.GetBytes(password).Select(x => x.ToString()));
        }

        public bool VerifyPassword(string hash, string password)
        {
            return hash == HashPassword(password);
        }
    }
}
