using Integral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IMeetingWebDataService : ICommonWebDataService<Meeting>
    {
        Task<Meeting?> CreateMeeting(int groupId, string theme, string? note = null);

        Task<Meeting?> ChangeMeetingNote(int meetingId, string? note = null);
    }
}
