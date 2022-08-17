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
            Uri uri = new(StudentPath + $"?groupId={groupId}&studentId={studentId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Post);
        }

        public async Task<bool> ChangeLeader(int groupId, int leaderId)
        {
            Uri uri = new(LeaderPath + $"?groupId={groupId}&leaderId={leaderId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Put);
        }

        public async Task<Group?> CreateGroup(string name, int grade, int leaderId, GroupType groupType)
        {
            Uri uri = new(ControllerName + $"?name={name}&grade={grade}&leaderId={leaderId}&groupType={groupType}", UriKind.Relative);

            return await SendRequest<Group>(uri, HttpMethod.Post);
        }

        public async Task<bool> UnassignStudent(int groupId, int studentId)
        {
            Uri uri = new(StudentPath + $"?groupId={groupId}&studentId={studentId}", UriKind.Relative);

            return await SendRequest<bool>(uri, HttpMethod.Delete);
        }
    }
}
