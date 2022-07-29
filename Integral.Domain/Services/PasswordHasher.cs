namespace Integral.Domain.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public bool VerifyPassword(string hash, string password)
        {
            return hash == password;
        }
    }
}
