using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IGroupWebDataService : ICommonWebDataService<Group>
    {
        Task<IEnumerable<User>?> GetUsersThatCanOwnGroup(GroupType type);

        Task<Group?> CreateGroup(string name, int grade, int leaderId, GroupType groupType);

        Task<bool> ChangeLeader(int groupId, int leaderId);

        Task<bool> AssignStudent(int groupId, int studentId);

        Task<bool> UnassignStudent(int groupId, int studentId);

    }
}
