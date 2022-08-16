using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class GroupWebDataService : CommonWebDataService<Group>, IGroupWebDataService
    {
        protected override string ControllerName => "Groups";

        private string StudentPath => ControllerName + "/Students";
        private string LeaderPath => ControllerName;


        public GroupWebDataService(HttpClient client) : base(client)
        {
        }

        public async Task<bool> AssignStudent(int groupId, int studentId)
        {
            UriBuilder ub = new()
            {
                Path = StudentPath,
                Query = $"groupId={groupId}&studentId={studentId}"
            };

            return await SendRequest<bool>(ub.Uri, HttpMethod.Post);
        }

        public async Task<bool> ChangeLeader(int groupId, int leaderId)
        {
            UriBuilder ub = new()
            {
                Path = StudentPath,
                Query = $"groupId={groupId}&leaderId={leaderId}"
            };

            return await SendRequest<bool>(ub.Uri, HttpMethod.Put);
        }

        public async Task<Group?> CreateGroup(string name, int grade, int leaderId, GroupType groupType)
        {
            UriBuilder ub = new()
            {
                Path = ControllerName,
                Query = $"name={name}&grade={grade}&leaderId={leaderId}&groupType={groupType}"
            };

            return await SendRequest<Group>(ub.Uri, HttpMethod.Post);
        }

        public async Task<bool> UnassignStudent(int groupId, int studentId)
        {
            UriBuilder ub = new()
            {
                Path = LeaderPath,
                Query = $"groupId={groupId}&studentId={studentId}"
            };

            return await SendRequest<bool>(ub.Uri, HttpMethod.Delete);
        }
    }
}
