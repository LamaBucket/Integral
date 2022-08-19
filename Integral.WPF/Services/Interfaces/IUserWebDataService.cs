using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IUserWebDataService : ICommonWebDataService<User>
    {
        Task<User?> AddUserRole(int id, Role role);

        Task<User?> RemoveUserRole(int id, Role role);

        Task<User?> CreateUser(string username, string password);

        Task<User?> UpdatePassword(int id, string password);
    }
}
