using Integral.Domain.Models;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class MeetingWebDataService : CommonWebDataService<Meeting>, IMeetingWebDataService
    {
        protected override string ControllerName => "Meetings";

        public MeetingWebDataService(HttpClient client) : base(client)
        {
        }

        public async Task<Meeting?> ChangeMeetingNote(int meetingId, string? note = null)
        {
            UriBuilder ub = new()
            {
                Path = ControllerName,
                Query = $"meetingId={meetingId}&note={note}"
            };

            return await SendRequest<Meeting>(ub.Uri, HttpMethod.Put);
        }

        public async Task<Meeting?> CreateMeeting(int groupId, string theme, string? note = null)
        {
            UriBuilder ub = new()
            {
                Path = ControllerName,
                Query = $"groupId={groupId}&theme={theme}&note={note}"
            };

            return await SendRequest<Meeting>(ub.Uri, HttpMethod.Post);
        }
    }
}
