namespace Integral.Domain.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);

        bool VerifyPassword(string hash, string password);
    }
}
