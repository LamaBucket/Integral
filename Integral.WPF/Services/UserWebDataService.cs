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


        public async Task<User?> AddUserRole(int id, Role role)
        {
            Uri uri = new(RoleEndpoint + $"?id={id}&userRole={role}", UriKind.Relative);

            return await SendRequest<User?>(uri, HttpMethod.Post);
        }

        public async Task<User?> RemoveUserRole(int id, Role role)
        {
            Uri uri = new(RoleEndpoint + $"?id={id}&userRole={role}", UriKind.Relative);

            return await SendRequest<User?>(uri, HttpMethod.Delete);
        }

        public async Task<User?> CreateUser(string username, string password)
        {
            Uri uri = new(CreateEndpoint + $"?id={username}&password={password}", UriKind.Relative);

            return await SendRequest<User>(uri, HttpMethod.Post);
        }

        public async Task<User?> UpdatePassword(int id, string password)
        {
            Uri uri = new(UpdateEndpoint + $"?id={id}&userRole={password}", UriKind.Relative);

            return await SendRequest<User>(uri, HttpMethod.Put);
        }
    }
}
