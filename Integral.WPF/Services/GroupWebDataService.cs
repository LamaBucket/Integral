using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class GroupWebDataService : CommonWebDataService<Group>, IGroupWebDataService
    {
        protected override string ControllerName => "Groups";


        private string StudentEndpoint => ControllerName + "/Students";

        private string LeaderEndpoint => ControllerName;

        private string UsersEndpoint => ControllerName + "/Users";


        public GroupWebDataService(HttpClient client) : base(client)
        {
        }

        public async Task<bool> AssignStudent(int groupId, int studentId)
        {
            Uri uri = new(StudentEndpoint + $"?groupId={groupId}&studentId={studentId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Post);
        }

        public async Task<bool> ChangeLeader(int groupId, int leaderId)
        {
            Uri uri = new(LeaderEndpoint + $"?groupId={groupId}&leaderId={leaderId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Put, false);
        }

        public async Task<Group?> CreateGroup(string name, int grade, int leaderId, GroupType groupType)
        {
            Uri uri = new(ControllerName + $"?name={name}&grade={grade}&leaderId={leaderId}&groupType={groupType}", UriKind.Relative);

            return await SendRequest<Group>(uri, HttpMethod.Post);
        }

        public async Task<bool> UnassignStudent(int groupId, int studentId)
        {
            Uri uri = new(StudentEndpoint + $"?groupId={groupId}&studentId={studentId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Delete);
        }

        public async Task<IEnumerable<User>?> GetUsersThatCanOwnGroup(GroupType type)
        {
            Uri uri = new(UsersEndpoint + $"?type={type}", UriKind.Relative);

            return await SendRequest<IEnumerable<User>>(uri, HttpMethod.Get);
        }
    }
}
