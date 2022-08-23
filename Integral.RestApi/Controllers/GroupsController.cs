using Integral.Domain;
using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using Integral.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Integral.RestApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private IGroupDataService _groupsDataService;
        private IDataService<Student> _studentsDataService;
        private IUserDataService _userDataService;

        public GroupsController(IGroupDataService groupDataService, IDataService<Student> studentsDataService, IUserDataService userDataService)
        {
            _groupsDataService = groupDataService;
            _studentsDataService = studentsDataService;
            _userDataService = userDataService;
        }

        private string? _username => User?.Identity?.Name;



        [Authorize(Roles = "Admin, Authority, Teacher, ClassPrincipal")]
        [HttpGet]
        public async Task<ActionResult> GetData(int? id = null)
        {
            int userId = await _userDataService.GetId(_username);

            bool flag = User.IsInRole("Admin") || User.IsInRole("Authority");

            object? data;

            if (!id.HasValue)
            {
                if (flag)
                    data = await _groupsDataService.GetAll();
                else
                {
                    Role? role = User.IsInRole("Teacher") ? Role.Teacher : (User.IsInRole("ClassPrincipal") ? Role.ClassPrincipal : null);

                    if (role is null)
                        return StatusCode(500);

                   data = await _groupsDataService.GetOwnedGroups(userId, role.Value);
                }
            }
            else
            {
                data = await _groupsDataService.Get(id.Value);
            }

            if (data is null)
                return NoContent();
            else
                return Json(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Users")]
        public async Task<ActionResult> GetUsersThatCanOwnGroup(GroupType type)
        {
            IEnumerable<User>? users = await _groupsDataService.GetUsersThatCanOwnGroup(type);

            return users is null ? NoContent() : Json(users);
        }


        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            Group? group = await _groupsDataService.Get(id);

            int userId = await _userDataService.GetId(_username);

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);


            if (User.IsInRole("Teacher") && group.LeaderId != userId)
                return Forbid();

            return Ok(await _groupsDataService.Delete(id));
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public async Task<ActionResult> CreateGroup(string name, [Range(1, 11)] int grade, int leaderId, GroupType groupType)
        {
            Role? requiredRole = null;

            switch (groupType)
            {
                case GroupType.Class:
                    requiredRole = Role.Admin;
                    break;
                case GroupType.ElectiveGroup:
                    requiredRole = Role.Teacher;
                    break;
            }

            if (requiredRole is null)
                return StatusCode(500);

            if (!await _userDataService.HasRole(leaderId, requiredRole.Value))
                return Forbid();

            if (User.IsInRole("Teacher"))
            {
                int userId = await _userDataService.GetId(_username);

                if (leaderId != userId)
                    return Forbid();
            }


            if (await _groupsDataService.ItemExists(name, grade))
                return BadRequest(ApiErrorCodes.GroupAlreadyExists);


            Group entity = new(name, grade, groupType);

            entity.LeaderId = leaderId;

            Group result = await _groupsDataService.Create(entity);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> ChangeLeader(int groupId, int leaderId)
        {
            Group? group = await _groupsDataService.Get(groupId);

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            Role? requiredRole = null;

            switch (group.GroupType)
            {
                case GroupType.Class:
                    requiredRole = Role.ClassPrincipal;
                    break;
                case GroupType.ElectiveGroup:
                    requiredRole = Role.Teacher;
                    break;
            }

            if (requiredRole is null)
                return StatusCode(500);

            if (!await _userDataService.HasRole(leaderId, requiredRole.Value))
                return Forbid();


            Group? result = await _groupsDataService.ChangeLeader(groupId, leaderId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost("Students")]
        public async Task<ActionResult> AssignStudent(int groupId, int studentId)
        {

            Group? group = await _groupsDataService.Get(groupId);
            Student? student = await _studentsDataService.Get(studentId);

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            if(student is null)
                return BadRequest(ApiErrorCodes.StudentNotExist);

            if (group.Students?.Select(x => x.Id).Contains(student.Id) ?? false)
                return BadRequest(ApiErrorCodes.StudentAlreadyInGroup);

            if (User.IsInRole("Teacher"))
            {
                int userId = await _userDataService.GetId(_username);

                if (group.LeaderId != userId)
                    return Forbid();
            }

            return Ok(await _groupsDataService.AssignStudent(groupId, studentId));
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete("Students")]
        public async Task<ActionResult> UnassignStudent(int groupId, int studentId)
        {
            Group? group = await _groupsDataService.Get(groupId);
            Student? student = await _studentsDataService.Get(studentId);

            if (group is null)
                return BadRequest(ApiErrorCodes.GroupNotExist);

            if (student is null)
                return BadRequest(ApiErrorCodes.StudentNotExist);

            if (!group.Students?.Select(x => x.Id).Contains(student.Id) ?? true)
                return BadRequest(ApiErrorCodes.StudentNotInGroup);

            if (User.IsInRole("Teacher"))
            {
                int userId = await _userDataService.GetId(_username);

                if (group.LeaderId != userId)
                    return Forbid();
            }

            if (group.Students is null)
                return BadRequest(ApiErrorCodes.GroupNotHasStudents);

            return Ok(await _groupsDataService.UnassignStudent(groupId, studentId));
        }
    }
}
