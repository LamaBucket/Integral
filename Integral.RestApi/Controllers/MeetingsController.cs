using Integral.Domain;
using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MeetingsController : Controller
    {
        private IMeetingDataService _meetingsDataService;
        private IGroupDataService _groupsDataService;
        private IUserDataService _userDataService;

        public MeetingsController(IMeetingDataService meetingsDataService, IGroupDataService groupsDataService, IUserDataService userDataService)
        {
            _meetingsDataService = meetingsDataService;
            _groupsDataService = groupsDataService;
            _userDataService = userDataService;
        }

        private string _username => User?.Identity?.Name ?? String.Empty;



        [Authorize(Roles = "Admin, Authority, Teacher, ClassPrincipal")]
        [HttpGet("Group")]
        public async Task<ActionResult> GetData(int groupId)
        {
            if (!await _groupsDataService.ItemExists(groupId))
                return BadRequest(ApiErrorCodes.GroupNotExist);

            IEnumerable<Meeting>? meetings = await _meetingsDataService.GetAllInGroup(groupId);

            if (meetings is null)
                return NoContent();

            return Json(meetings);
        }

        [Authorize(Roles = "Admin, Teacher, ClassPrincipal")]
        [HttpPost("Group")]
        public async Task<ActionResult> CreateMeeting(int groupId, string theme, DateTime date, string? note = null)
        {
            Group? group = await _groupsDataService.Get(groupId);

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            if (User.IsInRole("Teacher") || User.IsInRole("ClassPrincipal"))
            {
                int userId = await _userDataService.GetId(_username);

                if (group.LeaderId != userId)
                    return Forbid();
            }


            Meeting entity = new(theme, note, date)
            {
                GroupId = groupId
            };

            Meeting result = await _meetingsDataService.Create(entity);

            return Ok(result);
        }

        [Authorize(Roles = "Admin, Teacher, ClassPrincipal")]
        [HttpDelete]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            if (!await _meetingsDataService.ItemExists(id))
                return BadRequest(ApiErrorCodes.MeetingNotExist);

            Group? group = (await _meetingsDataService.Get(id))?.Group;

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            if (User.IsInRole("Teacher") || User.IsInRole("ClassPrincipal"))
            {
                int userId = await _userDataService.GetId(_username);

                if (group.LeaderId != userId)
                    return Forbid();
            }

            return Ok(await _meetingsDataService.Delete(id));
        }

        [Authorize(Roles = "Admin, Authority, Teacher, ClassPrincipal")]
        [HttpPost]
        public async Task<ActionResult> ChangeMeetingNote(int id, string? note = null)
        {
            Meeting? meeting = await _meetingsDataService.Get(id);

            if (meeting is null)
                return BadRequest(ApiErrorCodes.MeetingNotExist);

            Group? group = meeting.Group;

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            if (User.IsInRole("Teacher") || User.IsInRole("ClassPrincipal"))
            {
                int userId = await _userDataService.GetId(_username);

                if (group.LeaderId != userId)
                    return Forbid();
            }

            meeting.Note = note;

            Meeting result = await _meetingsDataService.Update(id, meeting);

            return Ok(result);
        }
    }
}
