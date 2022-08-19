using Integral.Domain.Models;
using Integral.Domain.Models.Enums;

namespace Integral.Domain.Services
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User?> Get(string username);

        Task<User?> AssignRole(int userId, Role role);

        Task<User?> UnassignRole(int userId, Role role);
    }
}
