using Integral.Domain.Models;

namespace Integral.Domain.Services
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User?> Get(string username);
    }
}
