using Integral.Domain.Models;
using Integral.Domain.Models.Enums;

namespace Integral.Domain.Services
{
    public interface IGroupDataService : IDataService<Group>
    {
        Task<IEnumerable<Group>?> GetOwnedGroups(int leaderId, Role role);

        Task<Group?> Get(string name, int grade);
    }
}
