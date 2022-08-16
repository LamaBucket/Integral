using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class UserWebDataService : CommonWebDataService<User>, IUserWebDataService
    {
        public UserWebDataService(HttpClient client) : base(client)
        {
        }

        protected override string ControllerName => "Users";

        public string RoleEndpoint => ControllerName + "/Role";


        public async Task<bool> AddUserRole(int id, Role role)
        {
            UriBuilder ub = new()
            {
                Path = RoleEndpoint,
                Query = $"id={id}&userRole={role}"
            };

            return await SendRequest<bool>(ub.Uri, HttpMethod.Post);
        }

        public async Task<bool> RemoveUserRole(int id, Role role)
        {
            UriBuilder ub = new()
            {
                Path = RoleEndpoint,
                Query = $"id={id}&userRole={role}"
            };

            return await SendRequest<bool>(ub.Uri, HttpMethod.Delete);
        }

        public async Task<User?> CreateUser(string username, string password)
        {
            UriBuilder ub = new()
            {
                Path = RoleEndpoint,
                Query = $"id={username}&password={password}"
            };

            return await SendRequest<User>(ub.Uri, HttpMethod.Post);
        }

        public async Task<User?> UpdatePassword(int id, string password)
        {
            UriBuilder ub = new()
            {
                Path = ControllerName,
                Query = $"id={id}&userRole={password}"
            };

            return await SendRequest<User>(ub.Uri, HttpMethod.Put);
        }
    }
}
