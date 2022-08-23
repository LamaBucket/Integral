using Integral.Domain.Models;
using Integral.Domain.Models.Enums;

namespace Integral.Domain.Services
{
    public interface IGroupDataService : IDataService<Group>
    {
        Task<bool> AssignStudent(int groupId, int studentId);

        Task<bool> UnassignStudent(int groupId, int studentId);

        Task<Group?> ChangeLeader(int groupId, int leaderId);

        Task<IEnumerable<User>?> GetUsersThatCanOwnGroup(GroupType type);

        Task<IEnumerable<Group>?> GetOwnedGroups(int leaderId, Role role);

        Task<Group?> Get(string name, int grade);
    }
}
